using Newtonsoft.Json;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace EgeBilgiTaskCase.WebUI.HelperServices
{
	public class UploadService
	{
		private IHttpContextAccessor _accessor;
		private readonly IWebHostEnvironment _webHostEnvironment;
		//	private readonly IServiceManager _sm;

		public UploadService(IHttpContextAccessor accessor, IWebHostEnvironment webHostEnvironment)
		{
			_accessor = accessor;
			_webHostEnvironment = webHostEnvironment;
		}

		#region ###################################################### FILE OPTS
		public string GetFileTypeIcon(string FilePath)
		{
			string MyImg = "";
			string Prefix = "";

			if (FilePath != "" && FilePath != null)
			{
				MyImg = Prefix + "/Images/Icons/i_FileType_file.png";

				string[] MyFiles = FilePath.Split('.');
				string MyExt = MyFiles[1].ToLower();

				//FilePath Varsa
				if (MyExt.Contains("doc"))
				{
					MyImg = Prefix + "/Images/Icons/i_FileType_doc.png";
				}
				else if (MyExt.Contains("xls"))
				{
					MyImg = Prefix + "/Images/Icons/i_FileType_xls.png";
				}
				else if (MyExt.Contains("pdf"))
				{
					MyImg = Prefix + "/Images/Icons/i_FileType_pdf.png";
				}
				else if (MyExt.Contains("ppt"))
				{
					MyImg = Prefix + "~/Images/Icons/i_FileType_ppt.png";
				}
				else if (MyExt.Contains("zip"))
				{
					MyImg = Prefix + "/Images/Icons/i_FileType_zip.png";
				}
				else if (MyExt.Contains("rar"))
				{
					MyImg = Prefix + "/Images/Icons/i_FileType_rar.png";
				}
				else if (MyExt.Contains("jp") || MyExt.Contains("gif") || MyExt.Contains("tif") || MyExt.Contains("png") || MyExt.Contains("bmp"))
				{
					MyImg = Prefix + "/Images/Icons/i_FileType_image.png";
				}
			}
			return MyImg;
		}

		public string GetFileExtension(string FilePath)
		{
			return FilePath.Substring(FilePath.LastIndexOf('.') + 1).ToLower().Replace("ı", "i");
		}
		public string GetFileNameShort(string FilePath)
		{
			string MyResult = FilePath.Substring(FilePath.LastIndexOf('/') + 1);

			return MyResult;
		}
		public string GetAcceptedExtensions(string? What)
		{
			string MyExt = ".png, .jpg, .jpeg, .gif, .bmp, .tif, .tiff, " +
				".pdf, .doc, .docx, .ppt, .pptx, .xls, .xlsx, " +
				".mp4, .flv, .m4v, " +
				".zip, .rar";

			if (What.ToLower() == "image")
			{
				MyExt = ".png, .jpg, .jpeg, .gif, .bmp";
			}
			else if (What.ToLower() == "file")
			{
				MyExt = ".pdf, .doc, .docx, .ppt, .pptx, .xls, .xlsx, .zip, .rar, .tif, .tiff";
			}
			else if (What.ToLower() == "video")
			{
				MyExt = ".mp4, .flv, .m4v, .mov";
			}
			if (What.ToLower() == "excel")
			{
				MyExt = ".xls, .xlsx";
			}

			return MyExt;
		}

		public string UploadFile(IFormFile? file, int maxwidth, string FilePrefix, string Folder)
		{
			string FileURL = "";

			if (file != null)
			{
				#region ------------------- Yükleme var ise
				string UploadPath1 = Path.Combine(_webHostEnvironment.WebRootPath, "images/Uploads/" + Folder);
				if (!Directory.Exists(UploadPath1))
				{
					Directory.CreateDirectory(UploadPath1);
				}

				string FileExtension = GetFileExtension(file.FileName);

				bool isImage = GetAcceptedExtensions("image").Contains(FileExtension);

				string filename = FilePrefix + "_" + DateTime.Now.ToString("yyyymmddhhssfff") + "." + FileExtension;

				string filePath = Path.Combine(UploadPath1, filename);

				if (isImage)
				{
					if (maxwidth == 0)
					{
						maxwidth = 1600;
					}

					using (SixLabors.ImageSharp.Image image = SixLabors.ImageSharp.Image.Load(file.OpenReadStream()))
					{
						int width = image.Width;
						int height = image.Height;

						if (width > maxwidth)
						{
							double MyScale = (double)width / (double)height;
							width = maxwidth;
							height = Convert.ToInt32(maxwidth / MyScale);
						}

						image.Mutate(x => x.Resize(width, height));

						image.Save(filePath);
					}
				}
				else
				{
					using (FileStream fs = new FileStream(filePath, FileMode.Create))
					{
						file.CopyTo(fs);
					}
				}

				if (!string.IsNullOrEmpty(filename))
				{
					FileURL = "/images/Uploads/" + Folder + "/" + filename;
				}

				#endregion
			}

			return FileURL;
		}

		public string GetUploadPath(string Folder)
		{
			string UploadPath1 = Path.Combine(_webHostEnvironment.WebRootPath, "images/Uploads/" + Folder);
			if (!Directory.Exists(UploadPath1))
			{
				Directory.CreateDirectory(UploadPath1);
			}
			return UploadPath1;
		}

		public string GetAbsPath(string FilePath)
		{
			string AA = _webHostEnvironment.WebRootPath;
			string BB = Path.Combine(AA, FilePath.TrimStart('/').Replace("/", "\\"));

			return BB;
		}

		public bool DeleteFilePhysically(int FFileID, string FileURL)
		{
			bool myresult = false;

			if (FFileID > 0 && string.IsNullOrEmpty(FileURL))
			{
				//var file = _sm.FFiles.GetEntity(a => a.FFileID == FFileID);
				//if (file != null)
				//{
				//	FileURL = file.FileURL;

				//	string deletePath = GetAbsPath(FileURL);
				//	FileInfo myfile = new FileInfo(deletePath);
				//	if (myfile.Exists)
				//	{
				//		System.IO.File.Delete(deletePath);
				//	}
				//}
			}

			if (!string.IsNullOrEmpty(FileURL))
			{
				string deletePath = GetAbsPath(FileURL);
				FileInfo myfile = new FileInfo(deletePath);
				if (myfile.Exists)
				{
					System.IO.File.Delete(deletePath);
				}
			}


			return myresult;
		}

		public IFormFile ConvertBase64ToFormFile(string base64String, string formFileName, string fileName, string fileType)
		{
			byte[] fileBytes = Convert.FromBase64String(base64String);
			var stream = new MemoryStream(fileBytes);
			var formFile = new FormFile(stream, 0, stream.Length, formFileName, fileName)
			{
				Headers = new HeaderDictionary(),
				ContentType = fileType
			};
			return formFile;
		}

		public async Task HandleFileDeletions(HttpContext httpContext)
		{
			var deletedFilesJson = httpContext.Session.GetString("DeletedFiles");
			if (!string.IsNullOrEmpty(deletedFilesJson))
			{
				var deletedFiles = JsonConvert.DeserializeObject<List<string>>(deletedFilesJson);
				if (deletedFiles.Any())
				{
					//foreach (var uploadNo in deletedFiles)
					//{
					//	var fileToDelete = await _sm.FFiles.GetEntityAsync(a => a.FileNo == uploadNo);
					//	if (fileToDelete != null)
					//	{
					//		await _sm.FFiles.DeleteAsync(fileToDelete);
					//	}
					//}
					//await _sm.SaveChangesAsync();
					//SessionHelper.ClearSession(httpContext.Session, "DeletedFiles");
				}
			}
		}

		#endregion


		#region ###################################################### QRCODE OPTS
		//public string CreateQRCode(string GUID, int ItemType)
		//{
		//	Guid qrGuid = Guid.NewGuid();

		//	string FileName = "Images/Uploads/QRCode/" + ItemType + "-" + qrGuid + ".png";

		//	string QRText = "https://EgeBilgiTaskCase.com/PDF/FileCheck?ItemType=" + ItemType + "&GUID=" + GUID;

		//	QrCodeEncodingOptions options = new()
		//	{
		//		DisableECI = true,
		//		CharacterSet = "UTF-8",
		//		Width = 500,
		//		Height = 500,
		//		Margin = 0
		//	};

		//	BarcodeWriter writer = new()
		//	{
		//		Format = BarcodeFormat.QR_CODE,
		//		Options = options
		//	};

		//	var bitmap = writer.Write(QRText);

		//	string UploadPath1 = Path.Combine(_webHostEnvironment.WebRootPath, FileName);

		//	bitmap.Save(UploadPath1);

		//	return "/" + FileName;
		//}
		#endregion
	}
}
