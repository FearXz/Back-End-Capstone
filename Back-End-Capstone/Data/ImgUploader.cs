﻿namespace Back_End_Capstone.Data
{
    public class ImgUploader
    {
        public string UploadFile(IFormFile file, string folderName, HttpContext httpContext)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("Nessuna immagine fornita");
            }

            var wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var fileName = Path.GetRandomFileName() + Path.GetExtension(file.FileName);
            var imagePath = Path.Combine(wwwrootPath, folderName, fileName);

            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            var host = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}";
            return $"{host}/{folderName}/{fileName}";
        }

        public void DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
