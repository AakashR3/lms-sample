using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace Tata.IGetIT.Learner.Service.Helpers
{
    public class SamlHelper
    {
        public string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public string GetEmailID(XmlDocument xmlDoc)
        {
            XmlNamespaceManager manager = new XmlNamespaceManager(xmlDoc.NameTable);
            manager.AddNamespace("ds", SignedXml.XmlDsigNamespaceUrl);
            manager.AddNamespace("saml", "urn:oasis:names:tc:SAML:2.0:assertion");
            manager.AddNamespace("samlp", "urn:oasis:names:tc:SAML:2.0:protocol");

            XmlNode node = xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:Subject/saml:NameID", manager);
            return node.InnerText;
        }
        public string GetDomain(string samlResponse)
        {

            try
            {
                string rawSAMLResponseXml = Base64Decode(samlResponse);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(rawSAMLResponseXml);
                string EmailID = GetEmailID(xmlDoc);
                string[] splitString = EmailID.Split('@');
                string DomainName = splitString[1];
                return DomainName;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
    public class Certificate
    {
        public X509Certificate2 cert;
        public void LoadCertificate(string certificate)
        {
            byte[] data = Convert.FromBase64String(certificate);
            // LoadCertificate(StringToByteArray(certificate));
            LoadCertificate(data);

        }
        public void LoadCertificate(byte[] certificate)
        {
            try
            {
                cert = new X509Certificate2(certificate);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to load certificate", ex);
            }
        }
        private byte[] StringToByteArray(string st)
        {
            byte[] bytes = new byte[st.Length];
            for (int i = 0; i < st.Length; i++)
            {
                bytes[i] = (byte)st[i];
            }
            return bytes;
        }
    }
    public class SamlResponse
    {
        private XmlDocument xmlDoc;
        private SamlSettings accountSettings;
        private Certificate certificate;
        public SamlResponse(SamlSettings accountSettings)
        {
            this.accountSettings = accountSettings;
            certificate = new Certificate();
            certificate.LoadCertificate(accountSettings.certificate);
        }

        public void LoadXml(string xml)
        {
            xmlDoc = new XmlDocument();
            xmlDoc.PreserveWhitespace = true;
            xmlDoc.XmlResolver = null;
            xmlDoc.LoadXml(xml);
        }

        public void LoadXmlFromBase64(string response)
        {
            string decode = SamlEncoding.DecodeSAMLResponse(response);
            LoadXml(decode);
        }

        public SamlAuthResponse GetBusinessDetails(SamlResponse samlResponse)
        {
            SamlAuthResponse samlAuthResponse = new();
            try
            {
                string ssoNameID = samlResponse.GetNameID();
                if (string.IsNullOrEmpty(ssoNameID.Trim()))
                    ssoNameID = string.Empty;

                samlAuthResponse.ssoNameID = ssoNameID;
            }
            catch
            {
                samlAuthResponse.ssoNameID = string.Empty;
            }

            try
            {
                string ssoUserID = samlResponse.GetUserID();
                if (string.IsNullOrEmpty(ssoUserID.Trim()))
                    ssoUserID = string.Empty;

                samlAuthResponse.ssoUserID = ssoUserID;
            }
            catch
            {
                samlAuthResponse.ssoUserID = string.Empty;
            }

            try
            {
                string ssoReqID = samlResponse.GetSSORequestID();
                if (string.IsNullOrEmpty(ssoReqID.Trim()))
                    ssoReqID = string.Empty;

                samlAuthResponse.ssoReqID = ssoReqID;
            }
            catch
            {
                samlAuthResponse.ssoReqID = string.Empty;
            }

            try
            {
                string ssoFirstName = samlResponse.GetFirstName();
                if (string.IsNullOrEmpty(ssoFirstName.Trim()))
                    ssoFirstName = string.Empty;

                samlAuthResponse.ssoFirstName = ssoFirstName;
            }
            catch
            {
                samlAuthResponse.ssoFirstName = string.Empty;
            }

            try
            {
                string ssoLastName = samlResponse.GetLastName();
                if (string.IsNullOrEmpty(ssoLastName.Trim()))
                    ssoLastName = string.Empty;

                samlAuthResponse.ssoLastName = ssoLastName;
            }
            catch
            {
                samlAuthResponse.ssoLastName = string.Empty;
            }
            try
            {
                string ssoCompany = samlResponse.GetCompany();
                if (string.IsNullOrEmpty(ssoCompany.Trim()))
                    ssoCompany = string.Empty;

                samlAuthResponse.ssoCompany = ssoCompany;
            }
            catch
            {
                samlAuthResponse.ssoCompany = string.Empty;
            }

            try
            {
                string ssoShipCountry = samlResponse.GetShipCountry();
                if (string.IsNullOrEmpty(ssoShipCountry.Trim()))
                    ssoShipCountry = string.Empty;

                samlAuthResponse.ssoShipCountry = ssoShipCountry;
            }
            catch
            {
                samlAuthResponse.ssoShipCountry = string.Empty;
            }

            try
            {
                string ssoBusinessSite = samlResponse.GetBusinessSite();
                if (string.IsNullOrEmpty(ssoBusinessSite.Trim()))
                    ssoBusinessSite = string.Empty;

                samlAuthResponse.ssoBusinessSite = ssoBusinessSite;
            }
            catch
            {
                samlAuthResponse.ssoBusinessSite = string.Empty;
            }

            try
            {
                string ssoBusinessGroup = samlResponse.GetBusinessGroup();
                if (string.IsNullOrEmpty(ssoBusinessGroup.Trim()))
                    ssoBusinessGroup = string.Empty;

                samlAuthResponse.ssoBusinessGroup = ssoBusinessGroup;
            }
            catch
            {
                samlAuthResponse.ssoBusinessGroup = string.Empty;
            }

            try
            {
                string ssoAttribute1 = samlResponse.GetAttribute1();
                if (string.IsNullOrEmpty(ssoAttribute1.Trim()))
                    ssoAttribute1 = string.Empty;

                samlAuthResponse.ssoAttribute1 = ssoAttribute1;
            }
            catch
            {
                samlAuthResponse.ssoAttribute1 = string.Empty;
            }

            try
            {
                string ssoAttribute2 = samlResponse.GetAttribute2();
                if (string.IsNullOrEmpty(ssoAttribute2.Trim()))
                    ssoAttribute2 = string.Empty;

                samlAuthResponse.ssoAttribute2 = ssoAttribute2;
            }
            catch
            {
                samlAuthResponse.ssoAttribute2 = string.Empty;
            }

            try
            {
                string ssoAttribute3 = samlResponse.GetAttribute3();
                if (string.IsNullOrEmpty(ssoAttribute3.Trim()))
                    ssoAttribute3 = string.Empty;

                samlAuthResponse.ssoAttribute3 = ssoAttribute3;
            }
            catch
            {
                samlAuthResponse.ssoAttribute3 = string.Empty;
            }

            try
            {
                string ssoAttribute4 = samlResponse.GetAttribute4();
                if (string.IsNullOrEmpty(ssoAttribute4.Trim()))
                    ssoAttribute4 = string.Empty;

                samlAuthResponse.ssoAttribute4 = ssoAttribute4;
            }
            catch
            {
                samlAuthResponse.ssoAttribute4 = string.Empty;
            }

            try
            {
                string ssoJobRole = samlResponse.GetJobRole();
                if (string.IsNullOrEmpty(ssoJobRole.Trim()))
                    ssoJobRole = string.Empty;

                samlAuthResponse.ssoJobRole = ssoJobRole;
            }
            catch
            {
                samlAuthResponse.ssoJobRole = string.Empty;
            }

            try
            {
                string ssoRegion = samlResponse.GetRegion();
                if (string.IsNullOrEmpty(ssoRegion.Trim()))
                    ssoRegion = string.Empty;

                samlAuthResponse.ssoRegion = ssoRegion;
            }
            catch
            {
                samlAuthResponse.ssoRegion = string.Empty;
            }

            samlAuthResponse.ssoUserID = samlAuthResponse.ssoNameID;

            return samlAuthResponse;
        }


        public bool IsValid()
        {
            bool status = false;

            XmlNamespaceManager manager = new XmlNamespaceManager(xmlDoc.NameTable);
            manager.AddNamespace("ds", SignedXml.XmlDsigNamespaceUrl);
            XmlNodeList nodeList = xmlDoc.SelectNodes("//ds:Signature", manager);

            SignedXml signedXml = new SignedXml(xmlDoc);
            XmlNode node = nodeList[0];
            signedXml.LoadXml((XmlElement)node);
            status = signedXml.CheckSignature(certificate.cert, true);
            if (!status)
                return false;
            else
                return status;
        }

        public string GetNameID()
        {
            try
            {
                XmlNamespaceManager manager = new XmlNamespaceManager(xmlDoc.NameTable);
                manager.AddNamespace("ds", SignedXml.XmlDsigNamespaceUrl);
                manager.AddNamespace("saml", "urn:oasis:names:tc:SAML:2.0:assertion");
                manager.AddNamespace("samlp", "urn:oasis:names:tc:SAML:2.0:protocol");

                XmlNode node = xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:Subject/saml:NameID", manager);

                if (node != null)
                {
                    return node.InnerText;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch
            {
                throw;
            }
        }


        public string GetUserID()
        {
            try
            {
                XmlNamespaceManager manager = new XmlNamespaceManager(xmlDoc.NameTable);
                manager.AddNamespace("ds", SignedXml.XmlDsigNamespaceUrl);
                manager.AddNamespace("saml", "urn:oasis:names:tc:SAML:2.0:assertion");
                manager.AddNamespace("samlp", "urn:oasis:names:tc:SAML:2.0:protocol");

                XmlNode node = xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:AttributeStatement/saml:Attribute[@Name='USERID']", manager);
                if (node != null)
                {
                    return node.InnerText;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch
            {
                throw;
            }
        }

        public string GetFirstName()
        {
            string strResult = "";
            try
            {
                XmlNamespaceManager manager = new XmlNamespaceManager(xmlDoc.NameTable);
                manager.AddNamespace("ds", SignedXml.XmlDsigNamespaceUrl);
                manager.AddNamespace("saml", "urn:oasis:names:tc:SAML:2.0:assertion");
                manager.AddNamespace("samlp", "urn:oasis:names:tc:SAML:2.0:protocol");

                XmlNode node = xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:AttributeStatement/saml:Attribute[@Name='FIRSTNAME']", manager);
                if (node != null)
                {
                    return node.InnerText;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch
            {
                return strResult;
            }
        }

        public string GetLastName()
        {
            string strResult = "";
            try
            {
                XmlNamespaceManager manager = new XmlNamespaceManager(xmlDoc.NameTable);
                manager.AddNamespace("ds", SignedXml.XmlDsigNamespaceUrl);
                manager.AddNamespace("saml", "urn:oasis:names:tc:SAML:2.0:assertion");
                manager.AddNamespace("samlp", "urn:oasis:names:tc:SAML:2.0:protocol");

                XmlNode node = xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:AttributeStatement/saml:Attribute[@Name='LASTNAME']", manager);
                if (node != null)
                {
                    return node.InnerText;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch
            {
                return strResult;
            }
        }

        public string GetCompany()
        {
            string strResult = "";
            try
            {
                XmlNamespaceManager manager = new XmlNamespaceManager(xmlDoc.NameTable);
                manager.AddNamespace("ds", SignedXml.XmlDsigNamespaceUrl);
                manager.AddNamespace("saml", "urn:oasis:names:tc:SAML:2.0:assertion");
                manager.AddNamespace("samlp", "urn:oasis:names:tc:SAML:2.0:protocol");

                XmlNode node = xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:AttributeStatement/saml:Attribute[@Name='COMPANY']", manager);
                if (node != null)
                {
                    return node.InnerText;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch
            {
                return strResult;
            }
        }

        public string GetShipCountry()
        {
            string strResult = "";
            try
            {
                XmlNamespaceManager manager = new XmlNamespaceManager(xmlDoc.NameTable);
                manager.AddNamespace("ds", SignedXml.XmlDsigNamespaceUrl);
                manager.AddNamespace("saml", "urn:oasis:names:tc:SAML:2.0:assertion");
                manager.AddNamespace("samlp", "urn:oasis:names:tc:SAML:2.0:protocol");

                XmlNode node = xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:AttributeStatement/saml:Attribute[@Name='COUNTRY']", manager);
                if (node != null)
                {
                    return node.InnerText;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch
            {
                return strResult;
            }
        }

        public string GetBusinessSite()
        {
            string strResult = "";
            try
            {
                XmlNamespaceManager manager = new XmlNamespaceManager(xmlDoc.NameTable);
                manager.AddNamespace("ds", SignedXml.XmlDsigNamespaceUrl);
                manager.AddNamespace("saml", "urn:oasis:names:tc:SAML:2.0:assertion");
                manager.AddNamespace("samlp", "urn:oasis:names:tc:SAML:2.0:protocol");

                XmlNode node = xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:AttributeStatement/saml:Attribute[@Name='BUSINESSSITE']", manager);
                if (node != null)
                {
                    return node.InnerText;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch
            {
                return strResult;
            }
        }

        public string GetBusinessGroup()
        {
            string strResult = "";
            try
            {
                XmlNamespaceManager manager = new XmlNamespaceManager(xmlDoc.NameTable);
                manager.AddNamespace("ds", SignedXml.XmlDsigNamespaceUrl);
                manager.AddNamespace("saml", "urn:oasis:names:tc:SAML:2.0:assertion");
                manager.AddNamespace("samlp", "urn:oasis:names:tc:SAML:2.0:protocol");

                XmlNode node = xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:AttributeStatement/saml:Attribute[@Name='BUSINESSGROUP']", manager);
                if (node != null)
                {
                    return node.InnerText;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch
            {
                return strResult;
            }
        }

        public string GetAttribute1()
        {
            string strResult = "";
            try
            {
                XmlNamespaceManager manager = new XmlNamespaceManager(xmlDoc.NameTable);
                manager.AddNamespace("ds", SignedXml.XmlDsigNamespaceUrl);
                manager.AddNamespace("saml", "urn:oasis:names:tc:SAML:2.0:assertion");
                manager.AddNamespace("samlp", "urn:oasis:names:tc:SAML:2.0:protocol");

                XmlNode node = xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:AttributeStatement/saml:Attribute[@Name='ATTRIBUTE1']", manager);
                if (node != null)
                {
                    return node.InnerText;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch
            {
                return strResult;
            }
        }

        public string GetAttribute2()
        {
            string strResult = "";
            try
            {
                XmlNamespaceManager manager = new XmlNamespaceManager(xmlDoc.NameTable);
                manager.AddNamespace("ds", SignedXml.XmlDsigNamespaceUrl);
                manager.AddNamespace("saml", "urn:oasis:names:tc:SAML:2.0:assertion");
                manager.AddNamespace("samlp", "urn:oasis:names:tc:SAML:2.0:protocol");

                XmlNode node = xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:AttributeStatement/saml:Attribute[@Name='ATTRIBUTE2']", manager);
                if (node != null)
                {
                    return node.InnerText;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch
            {
                return strResult;
            }
        }

        public string GetAttribute3()
        {
            string strResult = "";
            try
            {
                XmlNamespaceManager manager = new XmlNamespaceManager(xmlDoc.NameTable);
                manager.AddNamespace("ds", SignedXml.XmlDsigNamespaceUrl);
                manager.AddNamespace("saml", "urn:oasis:names:tc:SAML:2.0:assertion");
                manager.AddNamespace("samlp", "urn:oasis:names:tc:SAML:2.0:protocol");

                XmlNode node = xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:AttributeStatement/saml:Attribute[@Name='ATTRIBUTE3']", manager);
                if (node != null)
                {
                    return node.InnerText;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch
            {
                return strResult;
            }
        }

        public string GetAttribute4()
        {
            string strResult = "";
            try
            {
                XmlNamespaceManager manager = new XmlNamespaceManager(xmlDoc.NameTable);
                manager.AddNamespace("ds", SignedXml.XmlDsigNamespaceUrl);
                manager.AddNamespace("saml", "urn:oasis:names:tc:SAML:2.0:assertion");
                manager.AddNamespace("samlp", "urn:oasis:names:tc:SAML:2.0:protocol");

                XmlNode node = xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:AttributeStatement/saml:Attribute[@Name='ATTRIBUTE4']", manager);
                if (node != null)
                {
                    return node.InnerText;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch
            {
                return strResult;
            }
        }
        public string GetJobRole()
        {
            string strResult = "";
            try
            {
                XmlNamespaceManager manager = new XmlNamespaceManager(xmlDoc.NameTable);
                manager.AddNamespace("ds", SignedXml.XmlDsigNamespaceUrl);
                manager.AddNamespace("saml", "urn:oasis:names:tc:SAML:2.0:assertion");
                manager.AddNamespace("samlp", "urn:oasis:names:tc:SAML:2.0:protocol");

                XmlNode node = xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:AttributeStatement/saml:Attribute[@Name='JOBROLE']", manager);
                if (node != null)
                {
                    return node.InnerText;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch
            {
                return strResult;
            }
        }

        public string GetRegion()
        {
            string strResult = "";
            try
            {
                XmlNamespaceManager manager = new XmlNamespaceManager(xmlDoc.NameTable);
                manager.AddNamespace("ds", SignedXml.XmlDsigNamespaceUrl);
                manager.AddNamespace("saml", "urn:oasis:names:tc:SAML:2.0:assertion");
                manager.AddNamespace("samlp", "urn:oasis:names:tc:SAML:2.0:protocol");

                XmlNode node = xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:AttributeStatement/saml:Attribute[@Name='REGION']", manager);
                if (node != null)
                {
                    return node.InnerText;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch
            {
                return strResult;
            }
        }

        public string GetSSORequestID()
        {
            try
            {
                XmlNamespaceManager manager = new XmlNamespaceManager(xmlDoc.NameTable);
                manager.AddNamespace("ds", SignedXml.XmlDsigNamespaceUrl);
                manager.AddNamespace("saml", "urn:oasis:names:tc:SAML:2.0:assertion");
                manager.AddNamespace("samlp", "urn:oasis:names:tc:SAML:2.0:protocol");

                XmlNode node = xmlDoc.SelectSingleNode("/samlp:Response", manager);
                return node.Attributes["InResponseTo"].Value;
            }
            catch
            {
                return string.Empty;
            }
        }
    }

    public class AuthRequest
    {
        //Class objects
        private SamlSettings samlSettings;
        public string id;

        //Class constructor
        public AuthRequest(SamlSettings settings)
        {
            this.samlSettings = settings;
            id = "ID" + System.Guid.NewGuid().ToString();
        }

        //Saml AuthnRequest
        public string GetRequest()
        {
            using (StringWriter sw = new StringWriter())
            {
                XmlWriterSettings xws = new XmlWriterSettings();
                xws.OmitXmlDeclaration = true;

                using (XmlWriter xw = XmlWriter.Create(sw, xws))
                {
                    xw.WriteStartElement("samlp", "AuthnRequest", "urn:oasis:names:tc:SAML:2.0:protocol");
                    xw.WriteAttributeString("IssueInstant", DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ"));
                    xw.WriteAttributeString("ID", id/*"IDca9d3653d6f85fd24a4e0b51e5125cf1a8310a0e7afbcae603"*/);
                    xw.WriteAttributeString("Version", "2.0");
                    xw.WriteAttributeString("Destination", samlSettings.ssoUrl);
                    //xw.WriteAttributeString("AssertionConsumerServiceURL", samlSettings.entityID);
                    //xw.WriteAttributeString("ProtocolBinding", "urn:oasis:names:tc:SAML:2.0:bindings:HTTP-POST");

                    xw.WriteStartElement("saml", "Issuer", "urn:oasis:names:tc:SAML:2.0:assertion");
                    xw.WriteString(samlSettings.issuer);
                    xw.WriteEndElement();

                    //xw.WriteStartElement("samlp", "NameIDPolicy", "urn:oasis:names:tc:SAML:2.0:protocol");
                    //xw.WriteAttributeString("Format", "urn:oasis:names:tc:SAML:2.0:nameid-format:emailAddress");
                    //xw.WriteAttributeString("AllowCreate", "true");
                    //xw.WriteEndElement();

                    //xw.WriteStartElement("samlp", "RequestedAuthnContext", "urn:oasis:names:tc:SAML:2.0:protocol");
                    //xw.WriteAttributeString("Comparison", "exact");
                    //xw.WriteStartElement("saml", "AuthnContextClassRef", "urn:oasis:names:tc:SAML:2.0:assertion");
                    //xw.WriteString("urn:oasis:names:tc:SAML:2.0:ac:classes:PasswordProtectedTransport");
                    //xw.WriteEndElement();
                    //xw.WriteEndElement();

                    xw.WriteEndElement();
                }

                string tmp = sw.ToString();
                byte[] toEncodeAsBytes = System.Text.Encoding.Default.GetBytes(tmp);

                return SamlEncoding.GenerateSAMLRequestParam(tmp);
            }
        }
    }

    public static class SamlEncoding
    {
        //Encode request
        public static string GenerateSAMLRequestParam(string SAMLRequest)
        {
            var saml = string.Format(SAMLRequest, Guid.NewGuid());
            var bytes = System.Text.Encoding.UTF8.GetBytes(saml);
            using (var output = new MemoryStream())
            {
                using (var zip = new DeflateStream(output, CompressionMode.Compress))
                {
                    zip.Write(bytes, 0, bytes.Length);
                }
                var base64 = Convert.ToBase64String(output.ToArray());
                return HttpUtility.UrlEncode(base64);
            }
        }

        //Decode response
        public static string DecodeSAMLResponse(string response)
        {
            var utf8 = System.Text.Encoding.UTF8;
            var bytes = utf8.GetBytes(response);
            using (var output = new MemoryStream())
            {
                using (new DeflateStream(output, CompressionMode.Decompress))
                {
                    output.Write(bytes, 0, bytes.Length);
                }
                var base64 = utf8.GetString(output.ToArray());
                return utf8.GetString(Convert.FromBase64String(base64));
            }
        }

        #region Deprecated
        public static void CopyTo(Stream src, Stream dest)
        {
            byte[] bytes = new byte[4096];

            int cnt;

            while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0)
            {
                dest.Write(bytes, 0, cnt);
            }
        }

        public static byte[] Zip(string str)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(str);

            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(mso, CompressionMode.Compress))
                {
                    //msi.CopyTo(gs);
                    CopyTo(msi, gs);
                }

                return mso.ToArray();
            }
        }

        public static string Unzip(byte[] bytes)
        {
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    //gs.CopyTo(mso);
                    CopyTo(gs, mso);
                }

                return System.Text.Encoding.UTF8.GetString(mso.ToArray());
            }
        }
        #endregion
    }

}

