namespace SitemapEngine.Core.Framework
{
    public class SitemapSelector
    {
        public string Language { get; set; }
        public string Bundle { get; set; }
        public int Batch { get; set; }

        protected bool Equals(SitemapSelector other)
        {
            return string.Equals(Language, other.Language) && string.Equals(Bundle, other.Bundle) && Batch == other.Batch;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((SitemapSelector)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Language != null ? Language.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Bundle != null ? Bundle.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Batch;
                return hashCode;
            }
        }
    }
}
