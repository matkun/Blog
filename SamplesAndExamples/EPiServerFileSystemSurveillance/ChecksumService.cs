using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace EPiServer.Checksums
{
    public class ChecksumService
    {
        private const string ChecksumsFilename = "Checksums.txt";

        public static void VerifyConfigs()
        {
            var oldChecksums = OldChecksumsFromFile().ToDictionary(p => p.Key, p => p.Value);
            var currentChecksums = RecalculateChecksums().ToDictionary(p => p.Key, p => p.Value);

            string discrepancyReport;
            if (ValidateChecksums(oldChecksums, currentChecksums, out discrepancyReport))
            {
                return;
            }
            SendNotificationMail(discrepancyReport);
            WriteNewChecksums(currentChecksums);
        }

        private static IEnumerable<KeyValuePair<string, string>> OldChecksumsFromFile()
        {
            var filePath = Path.Combine(HttpRuntime.AppDomainAppPath, "App_Data", ChecksumsFilename);
            if (!File.Exists(filePath))
            {
                yield break;
            }
            foreach (var line in File.ReadAllLines(filePath).Where(l => !string.IsNullOrEmpty(l)))
            {
                var pair = line.Split('#');
                var configName = pair.Length > 0 ? pair[0] : string.Empty;
                var checksum = pair.Length > 1 ? pair[1] : string.Empty;
                yield return new KeyValuePair<string, string>(configName, checksum);
            }
        }

        private static IEnumerable<KeyValuePair<string, string>> RecalculateChecksums()
        {
            var configfiles = Directory
                .GetFiles(HttpRuntime.AppDomainAppPath, "*.config", SearchOption.AllDirectories)
                .Where(n => !string.IsNullOrEmpty(n));
            return from file in configfiles
                   let checksum = CalculateChecksumFor(File.ReadAllText(file))
                   select new KeyValuePair<string, string>(file, checksum);
        }

        private static bool ValidateChecksums(Dictionary<string, string> oldChecksums, Dictionary<string, string> currentChecksums, out string discrepancyReport)
        {
            var reportBuilder = new StringBuilder();

            if (oldChecksums.Count <= 0)
            {
                reportBuilder.AppendLine("The checksum file was either missing or empty.");
                reportBuilder.AppendLine(string.Empty);
            }

            var newConfigs = currentChecksums.Keys.Where(k => !oldChecksums.ContainsKey(k)).ToArray();
            if (newConfigs.Any())
            {
                reportBuilder.AppendLine("The following config file(s) did not have an old checksum in the checksum file (config(s) were recently added):");
                foreach (var filename in newConfigs)
                {
                    reportBuilder.AppendLine(string.Concat(" - File: ", filename));
                }
                reportBuilder.AppendLine(string.Empty);
            }

            var deletedConfigs = oldChecksums.Keys.Where(k => !currentChecksums.ContainsKey(k)).ToArray();
            if (deletedConfigs.Any())
            {
                reportBuilder.AppendLine("The following config file(s) did not exist on disk, even though there was a checksum for it in the checksum file (config(s) were recently deleted):");
                foreach (var filename in deletedConfigs)
                {
                    reportBuilder.AppendLine(string.Concat(" - File: ", filename));
                    reportBuilder.AppendLine(string.Concat(" - Old checksum: ", oldChecksums[filename]));
                }
                reportBuilder.AppendLine(string.Empty);
            }

            var discrepancies = currentChecksums.Keys
                .Where(oldChecksums.ContainsKey)
                .Where(file => !currentChecksums[file].Equals(oldChecksums[file]));
            foreach (var file in discrepancies)
            {
                reportBuilder.AppendLine(string.Concat("Checksum mismatch, config file has changed: ", file));
            }

            if (reportBuilder.Length > 0)
            {
                reportBuilder.AppendLine(string.Empty);
                reportBuilder.AppendLine("NOTE: If everything went well, the checksum file was rewritten with the current checksums after this mail was sent.");

                reportBuilder.AppendLine(string.Empty);
                reportBuilder.AppendLine(string.Concat("Machine name: ", Environment.MachineName));

                var hostName = Dns.GetHostName();
                reportBuilder.AppendLine(string.Concat("Host name: ", hostName));

                var ips = string.Join(", ",
                                      Dns.GetHostAddresses(hostName)
                                          .Where(ip => ip.AddressFamily == AddressFamily.InterNetwork) // Only get IPv4
                                          .Select(ip => ip.ToString())
                                          .ToArray());
                reportBuilder.AppendLine(string.Concat("Host IPs: ", ips));

                reportBuilder.AppendLine(string.Empty);
                reportBuilder.AppendLine("--- Old checksums ---");
                foreach (var pair in oldChecksums)
                {
                    reportBuilder.AppendFormat("{0}#{1}{2}", pair.Key, pair.Value, Environment.NewLine);
                }
            }

            discrepancyReport = reportBuilder.ToString();
            return string.IsNullOrEmpty(discrepancyReport);
        }

        private static void SendNotificationMail(string mailBody)
        {
            var toAddress = new MailAddress("mail@mathiaskunto.com", "Mathias Kunto");
            var fromAddress = new MailAddress("ProductionServers@gmail.com", "Production Servers");
            const string fromPassword = "R4c?.p4inEg5#34_Ds^^b4!!";
            var subject = string.Concat("Config(s) changed on ", Environment.MachineName);

            var smtpClient = new SmtpClient
                                 {
                                     Host = "smtp.gmail.com",
                                     Port = 587,
                                     EnableSsl = true,
                                     DeliveryMethod = SmtpDeliveryMethod.Network,
                                     UseDefaultCredentials = false,
                                     Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                                 };

            var mailMessage = new MailMessage(fromAddress, toAddress)
                                  {
                                      Subject = subject,
                                      Body = mailBody
                                  };
            try
            {
                smtpClient.Send(mailMessage);
            }
            catch (SmtpException e) { }
            catch {} // ...
        }

        private static void WriteNewChecksums(Dictionary<string, string> checksums)
        {
            var filePath = Path.Combine(HttpRuntime.AppDomainAppPath, "App_Data", ChecksumsFilename);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            var sums = checksums.Select(pair => string.Format("{0}#{1}", pair.Key, pair.Value));
            File.WriteAllLines(filePath, sums);
        }

        private static string CalculateChecksumFor(string input)
        {
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hash = MD5.Create().ComputeHash(inputBytes);

            var stringBuilder = new StringBuilder();
            foreach (var h in hash)
            {
                stringBuilder.Append(h.ToString("x2"));
            }
            return stringBuilder.ToString();
        }
    }
}
