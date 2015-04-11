Extending EPiServer MSBuild config transforms to provide server specific configuration for TCP EventReplication and Licenses
https://blog.mathiaskunto.com/2015/04/11/extending-episerver-msbuild-config-transforms-to-provide-server-specific-configuration-for-tcp-eventreplication-and-licenses/

Sample MSBuild setup for having server specifict configuration transformation for deploying different ones on each server automatically. This may be useful while for instance needing to use TCP for EPiServer's EventReplication service, working with ImageVault loadbalancing or just deploying the correct License.config file to the correct server in an easy fashion.
