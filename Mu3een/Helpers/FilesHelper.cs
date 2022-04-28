namespace Mu3een.Helpers
{
    public class FilesHelper
    {
        private readonly IWebHostEnvironment HostingEnvironment;

        public FilesHelper(IWebHostEnvironment hostingEnvironment)
        {
            this.HostingEnvironment = hostingEnvironment;
        }
        public async Task<string> UploadFile(IFormFile file, string uploadPath = "uploads/images")
        {
            string ext = file.FileName.Split(".")[1];
            string fName = $"{Guid.NewGuid()}.{ext}";
            string dirpath = Path.Combine(HostingEnvironment.WebRootPath, uploadPath);
            if (!Directory.Exists(dirpath))
            {
                Directory.CreateDirectory(dirpath);
            }
            string filePath = Path.Combine(dirpath, fName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return uploadPath + "/" + fName;
        }

        public void DeleteFile(string file, string uploadPath = "uploads/images")
        {
            string dirpath = Path.Combine(HostingEnvironment.WebRootPath, uploadPath);
            file = Path.Combine(dirpath, file);
            if (File.Exists(dirpath))
            {
                File.Delete(file);
            }
        }
    }
}
