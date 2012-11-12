using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Threading;


namespace CFResolve
{
	class MainClass
	{
		/*IPSubnet.cs supports IPv6
		Need to add support for it here though
		
		CloudFlare IP list(IPv4/6): https://www.cloudflare.com/ips*/		
		
		
	  static void Main(string[] args)
	  {
			Functions.InitColors();
			Functions.log ("Starting CF-Resolve ( github.com/Simran/CF-Resolve )", 4);
			
		    string[] list = { 
				"{0}", "cpanel.{0}", "webdisk.{0}", "forum.{0}", "forums.{0}", "dev.{0}", 
				"irc.{0}", "ns.{0}", "ns1.{0}", "pop.{0}", "smtp.{0}", "ns2.{0}", "cdn1.{0}",
				"mobilemail.{0}", "imap.{0}", "files.{0}", "fax.{0}", "ftp.{0}", "help.{0}",
				"whm.{0}", "email.{0}",  "mail.{0}", "webmail.{0}", "direct.{0}", "cdn.{0}", 
				"cdn2.{0}", "dns.{0}", "server.{0}", "record.{0}", "www.{0}", "contact.{0}",
				"direct-connect.{0}", "ssl.{0}", "blog.{0}", "dev.{0}", "ipv6.{0}", "{0}:2082", 
				"{0}:2083"
			};

			if (args.Length == 0) 
			{
				Functions.log ("Invalid DNS (Null)\n", 3);
				Environment.Exit(0);
			}
			
		    string sitedns = args[0];
			
		    if (sitedns != String.Empty)
    		{
				sitedns = sitedns.Replace("http://", String.Empty).Replace("www.", String.Empty).Replace("/", String.Empty);
				foreach (string item in list)
				{
					try
					{
					IPHostEntry iphost = Dns.GetHostEntry(string.Format (item, sitedns));
					
					foreach (IPAddress ip in iphost.AddressList)
						{
							checkCF(item, sitedns, ip.ToString());
						}
					}
					catch
					{
						Functions.log(string.Format ("[-]\t{0} - Nothing Found!", string.Format (item, sitedns)), 3);
					}
				}
				Environment.Exit(0);
    		}
    		else
    		{
    			Functions.log ("Invalid DNS (Null)\n", 3);
				Environment.Exit(0);
    		}
	  	}
		
		static void checkCF(string item, string sitedns, string ip)
		{
			string[] cfIPs = { "204.93.240.0/24", "204.93.177.0/24", 
				"199.27.128.0/21", "173.245.48.0/20",
				"103.22.200.0/22", "141.101.64.0/18",
				"108.162.192.0/18", "190.93.240.0/20", 
				"188.114.96.0/20" 
			};
			foreach (string ips in cfIPs)
			{
				if (new IPSubnet(ips).Contains(ip))
				{
					Functions.log(string.Format ("[*]\t{0} - {1}", string.Format (item, sitedns), ip), 1);
					return;
				}
			}
			Functions.log(string.Format ("[+]\t{0} - {1}", string.Format (item, sitedns), ip), 2);
		}
	}	
}
