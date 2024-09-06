using System.Globalization;
using System.Text.RegularExpressions;
using System.Web;

namespace EgeBilgiTaskCase.Client.Services
{
    public class AAAService
    {
        private IHttpContextAccessor _accessor;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AAAService(IHttpContextAccessor accessor, IWebHostEnvironment webHostEnvironment)
        {
            _accessor = accessor;
            _webHostEnvironment = webHostEnvironment;
        }

        #region ###################################################### URL / IP FUNCTIONS
        public string Get_AppURL(int T)
        {
            string URL = T == 0 ? "https://EgeBilgiTaskCase.com" : "http://localhost:7260";
            return URL;
        }
        public string Get_AppName()
        {
            string URL = "Ege Bilgi Yazilim";
            return URL;
        }
        public string Get_FileStartURL(string FolderName)
        {
            string URL = Get_AppURL(0) + "/Images/Uploads/" + FolderName + "/";
            return URL;
        }
        public string Get_RecentURL()
        {
            var absoluteUri = string.Concat(
                        _accessor.HttpContext.Request.Scheme,
                        "://",
                        _accessor.HttpContext.Request.Host.ToUriComponent(),
                        _accessor.HttpContext.Request.PathBase.ToUriComponent(),
                        _accessor.HttpContext.Request.Path.ToUriComponent(),
                        _accessor.HttpContext.Request.QueryString.ToUriComponent());

            return absoluteUri;
        }
        public string Get_UserIP()
        {
            string? UserIP = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();

            if (string.IsNullOrEmpty(UserIP))
            {
                UserIP = "Bilinmiyor";
            }
            if (UserIP == "::1")
            {
                UserIP = "7Aralik";
            }

            return UserIP;
        }
        public string GetAbsPath(string FilePath)
        {
            string AA = _webHostEnvironment.WebRootPath;
            string BB = Path.Combine(AA, FilePath.TrimStart('/').Replace("/", "\\"));

            return BB;
        }
        #endregion

        #region ###################################################### CHECK FUNCTIONS
        public bool IsValidNumeric(object Expression)
        {
            bool isNum;

            double retNum;

            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }
        public bool IsValidEmail(string strIn)
        {
            //return true; //if strIn is in valid e-mail format.

            return Regex.IsMatch(strIn,
                   @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
                   @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
        }
        public bool IsValidDate(string strIn)
        {
            bool MyResult = false;

            DateTime MyCheckValue;
            if (DateTime.TryParse(strIn, out MyCheckValue))
            {
                MyResult = true;
            }
            return MyResult;
        }
        public bool CheckFileExtension(int FileType, string FileContentName)
        {
            bool MyResult = false;

            string FileExtension = System.IO.Path.GetExtension(FileContentName).ToLower();

            if (FileType == 0)
            {
                if (FileExtension.Contains(".jpeg") ||
                FileExtension.Contains(".jpg") ||
                FileExtension.Contains(".png") ||
                FileExtension.Contains(".gif") ||
                FileExtension.Contains(".tif") ||
                FileExtension.Contains(".tiff") ||
                FileExtension.Contains(".bmp") ||
                FileExtension.Contains(".pdf") ||
                FileExtension.Contains(".ppt") ||
                FileExtension.Contains(".xls") ||
                FileExtension.Contains(".doc") ||
                FileExtension.Contains(".zip") ||
                FileExtension.Contains(".rar") ||
                FileExtension.Contains(".flv") ||
                FileExtension.Contains(".mp4") ||
                FileExtension.Contains(".wmv"))
                {
                    MyResult = true;
                }
            }
            else if (FileType == 1)
            {
                if (FileExtension.Contains(".jpeg") ||
                   FileExtension.Contains(".jpg") ||
                   FileExtension.Contains(".png") ||
                   FileExtension.Contains(".gif"))
                {
                    MyResult = true;
                }

            }
            return MyResult;
        }
        #endregion

        #region ###################################################### GENERIC FUNCTIONS

        //public async Task<CurrencyOpt> Get_PriceForCurrencies(double Price, double ExchangeRate_1, double ExchangeRate_20, int MainCurrencyID)
        //{
        //    CurrencyOpt myResult = new();

        //    if (MainCurrencyID == 0)
        //    {
        //        myResult.Value_0 = Price;
        //        myResult.Value_1 = Math.Round((Price * ExchangeRate_1), 3);
        //        myResult.Value_20 = Math.Round((Price * ExchangeRate_20), 3);
        //    }
        //    else if (MainCurrencyID == 1)
        //    {
        //        myResult.Value_0 = Math.Round((Price * ExchangeRate_1), 3);
        //        myResult.Value_1 = Price;
        //        myResult.Value_20 = Math.Round((Price * (ExchangeRate_1 / ExchangeRate_20)), 3);
        //    }
        //    else if (MainCurrencyID == 20)
        //    {
        //        myResult.Value_0 = Math.Round((Price * ExchangeRate_20), 3);
        //        myResult.Value_1 = Math.Round((Price * (ExchangeRate_20 / ExchangeRate_1)), 3);
        //        myResult.Value_20 = Price;
        //    }

        //    return myResult;
        //}
        public double Get_PriceForGivenCurrency(double Price, double ExchangeRate_1, double ExchangeRate_20, int MainCurrencyID, int TargetCurrencyID)
        {
            double MyValue = Price;

            if (MainCurrencyID == 160)
            {
                MainCurrencyID = 0;
            }
            if (TargetCurrencyID == 160)
            {
                TargetCurrencyID = 0;
            }

            if (Price != 0)
            {
                if (MainCurrencyID != TargetCurrencyID)
                {
                    if (TargetCurrencyID == 0)
                    {
                        #region ---------------------------- TRL
                        if (MainCurrencyID == 1)
                        {
                            MyValue = Price * ExchangeRate_1;
                        }
                        else if (MainCurrencyID == 20)
                        {
                            MyValue = Price * ExchangeRate_20;
                        }
                        #endregion
                    }
                    else if (TargetCurrencyID == 1)
                    {
                        #region ---------------------------- USD

                        if (MainCurrencyID == 0)
                        {
                            MyValue = Price / ExchangeRate_1;
                        }
                        else if (MainCurrencyID == 20)
                        {
                            MyValue = Price * (ExchangeRate_20 / ExchangeRate_1);
                        }

                        #endregion
                    }
                    else if (TargetCurrencyID == 20)
                    {
                        #region ---------------------------- EUR

                        if (MainCurrencyID == 0)
                        {
                            MyValue = Price / ExchangeRate_20;
                        }
                        else if (MainCurrencyID == 1)
                        {
                            MyValue = Price * (ExchangeRate_1 / ExchangeRate_20);
                        }

                        #endregion
                    }
                }
            }
            return Math.Round(MyValue, 3);
        }
        public string RemoveQueryStringByKey(string url, string key)
        {
            var uri = new Uri(url);

            // this gets all the query string key value pairs as a collection
            var newQueryString = HttpUtility.ParseQueryString(uri.Query);

            // this removes the key if exists
            newQueryString.Remove(key);

            // this gets the page path from root without QueryString
            string pagePathWithoutQueryString = uri.GetLeftPart(UriPartial.Path);

            return newQueryString.Count > 0
                ? String.Format("{0}?{1}", pagePathWithoutQueryString, newQueryString)
                : pagePathWithoutQueryString;
        }
        public string Get_TarihFormati_Sunucu()
        {
            string MyValue = "yyyy-MM-dd HH:mm";
            //string MyValue = "dd-MM-yyyy HH:mm";

            return MyValue;
        }
        public string Get_TarihFormati_Lokal()
        {
            string MyValue = "dd.MM.yyyy HH:mm";

            return MyValue;
        }
        public string Get_TarihFormati_LokalS()
        {
            string MyValue = "dd.MM.yyyy";
            //string MyValue = "dd-MM-yyyy HH:mm";

            return MyValue;
        }
        public string Format_TarihGrid(DateTime MyDate)
        {
            string MyValue = "";

            string[] DDD = MyDate.ToString("dd.MMMM.yyyy", new CultureInfo("en-GB")).Split('.');

            MyValue = "<div class='text-center' style='margin-bottom:-5px'><span class='F20' style='line-height:1px'>" + DDD[0] + "</span><br><span class='F11' style='line-height: 1.2; display:block;'>" + DDD[1] + "</span><span class='F9' style='color:#a7a7a7;line-height: 1'>" + DDD[2] + "</span></div>";
            //string MyValue = "dd-MM-yyyy HH:mm";

            return MyValue;
        }
        public int GetSessionHourDiff()
        {
            var value = _accessor.HttpContext.Session.GetString("HourDiff");

            return string.IsNullOrEmpty(value) ? 0 : Convert.ToInt32(value);
        }


        public string TCFormat(string TC)
        {
            string result = "***********";
            if (TC.Length == 11)
            {
                string first = TC.Substring(0, 2);
                string last = TC.Substring(9, 2);
                result = first + "*******" + last;
            }

            return result;
        }
        public string FormatPrice(double number)
        {
            return number.ToString("N2", new CultureInfo("is-IS"));
            // return number.ToString("#,##0");
            //return number.ToString("00.00");

            //https://www.freeformatter.com/turkey-standards-code-snippets.html
        }
        public string Get_Summary(string TextContent, int CharacterLimit)
        {
            string SD = "";
            if (TextContent.Length > CharacterLimit)
            {
                SD = TextContent.Substring(0, CharacterLimit);
            }
            else
            {
                SD = TextContent;
            }

            return SD;
        }
        public string Get_Summary2(string TextContent, int CharacterLimit)
        {
            string SD = "";
            if (TextContent.Length > CharacterLimit - 3)
            {
                SD = TextContent.Substring(0, CharacterLimit - 3) + "...";
            }
            else
            {
                SD = TextContent;
            }

            return SD;
        }
        public string GetSummaryClearHtml(string TextContent, int CharacterLimit)
        {
            Regex html = new Regex("<.*?>", RegexOptions.Compiled);
            string NewTextContent = html.Replace(TextContent, string.Empty);
            string SD = "";
            if (NewTextContent.Length > CharacterLimit)
            {
                SD = NewTextContent.Substring(0, CharacterLimit);
            }
            else
            {
                SD = NewTextContent;
            }

            return SD;
        }
        public string Get_TitleCase(string Text)
        {
            TextInfo textInfo = new CultureInfo("tr-TR", false).TextInfo;

            return textInfo.ToTitleCase(Text);
        }
        public string CheckSqlInjection_Criteria(string Check)
        {
            List<string> MYAA1 = new List<string>(new string[] { "'", "/", "\"", "\\", "<", ">" });
            foreach (string b in MYAA1)
            {
                Check = Check.Replace(b, "");
            }

            string Value = Check;
            string AAA = Check.ToLower().Trim();

            bool g1 = AAA.Contains("--");
            bool g2 = AAA.Contains("or 1=1");
            bool g3 = AAA.Contains("or 1 = 1");
            bool g4 = AAA.Contains("delete ");
            bool g5 = AAA.Contains("drop ");
            bool g6 = AAA.Contains("truncate ");

            if (g1 || g2 || g3 || g4 || g5 || g6)
            {
                Value = "1=0";
            }
            return Value;
        }
        public string FilterAllTRChars(string EMail)
        {
            return EMail.Trim().Replace("'", "").Replace("İ", "I").Replace("Ş", "S").Replace("Ç", "C").Replace("Ğ", "G").Replace("Ö", "O").Replace("Ü", "U").
                Replace("ş", "s").Replace("ı", "i").Replace("ö", "o").Replace("ü", "u").Replace("ç", "c").Replace("ğ", "g");
        }
        public string Get_Filteredname(string Text)
        {
            string MyResult = FilterAllTRChars(Text.ToLower()).Replace(" ", "_");

            return MyResult;
        }
        public double Get_KDV(string Opt, double KDVOrani, double Deger)
        {
            double MyResult = 0;

            if (Opt == "Tutar") // Deger = ToplamTutar
            {
                MyResult = Deger / (1 + KDVOrani);
            }
            if (Opt == "ToplamTutar") // Deger = Tutar
            {
                MyResult = Deger * (1 + KDVOrani);
            }
            if (Opt == "KDV") // Deger = Tutar
            {
                MyResult = Deger * KDVOrani;
            }

            return Math.Round(MyResult, 4);
        }

        #endregion

        public string? GetJWTToken()
        {
            var JWTToken = _accessor.HttpContext.Request.Cookies["JWTToken"];

            return JWTToken;
        }
    }
}
