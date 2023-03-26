using ECommerce.Application.Services;
using ECommerce.Infrastructure.Operations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Infrastructure.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<bool> CopyFileAsync(string path, IFormFile file)
        {
            try
            {
                await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, false);

                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return true;
            }
            catch (Exception ex)
            {
                //TODO Log!
                throw ex;
            }

        }

        //TODO FileRename Düzenelenecek
        //private static async Task<string> FileRenameAsync(string path, string fileName, bool first = true)
        //{
        //    var newFileName = await Task.Run<string>(async () =>
        //    {
        //        var extension = Path.GetExtension(fileName);
        //        var oldName = Path.GetFileNameWithoutExtension(fileName);
        //        var newName = NameOperation.CharacterRegulatory(oldName);
        //        var newFile = $"{newName}{extension}";
        //        var isFileExists = File.Exists(Path.Combine(path, newFile));
        //        if (!isFileExists)
        //        {
        //            return $"{newName}{extension}";
        //        }
        //        else
        //        {
        //            //ismin sonunda - mi var ?
        //            //evetse sayı ekle
        //            //hayırsa son terimi sil sayıyı arttır
        //            int index = newName.LastIndexOf("-");
        //            return string.Empty;
        //        }
        //    });
        //    return newFileName;
        //}


        private async Task<string> FileRenameAsync(string path, string fileName, bool first = true)
        {
            var newFileName = await Task.Run<string>(async () =>
                {
                    string extension = Path.GetExtension(fileName);
                    string newFileName = string.Empty;
                    if (!first)
                    {
                        string oldName = Path.GetFileNameWithoutExtension(fileName);
                        newFileName = $"{NameOperation.CharacterRegulatory(oldName)}.{extension}";
                    }
                    else
                    {
                        newFileName = fileName;
                        int indexNo1 = newFileName.IndexOf("-");
                        if (indexNo1 == -1)
                        {
                            newFileName = $"{Path.GetFileNameWithoutExtension(newFileName)}-2{extension}";
                        }
                        else
                        {
                            int lastIndex = 0;
                            while (true)
                            {
                                lastIndex = indexNo1;
                                indexNo1 = newFileName.IndexOf("-", indexNo1 + 1);
                                if (indexNo1 == -1)
                                {
                                    indexNo1 = lastIndex;
                                    break;
                                }
                            }

                            int indexNo2 = newFileName.IndexOf(".");
                            string fileNo = newFileName.Substring(indexNo1 + 1, indexNo2 - indexNo1 - 1);

                            if (int.TryParse(fileNo, out int _fileNo))
                            {
                                _fileNo++;
                                newFileName = newFileName.Remove(indexNo1 + 1, indexNo2 - indexNo1 - 1).Insert(indexNo1 + 1, _fileNo.ToString());
                            }
                            else
                            {
                                newFileName = $"{Path.GetFileNameWithoutExtension(newFileName)}-2{extension}";
                            }
                        }
                    }

                    if (File.Exists($"{path}\\{newFileName}"))
                        return await FileRenameAsync(path, newFileName, false);
                    else
                        return newFileName;
                });
            return newFileName;
        }

        public async Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files)
        {
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);
            if (!Directory.Exists(uploadPath)) Directory.CreateDirectory(uploadPath);

            List<bool> results = new();
            List<(string fileName, string path)> datas = new();

            foreach (IFormFile file in files)
            {
                var fileNewName = await FileRenameAsync(uploadPath, file.FileName);
                string fullPath = Path.Combine(uploadPath, fileNewName);
                var result = await CopyFileAsync(fullPath, file);
                datas.Add((fileNewName, Path.Combine(path, fileNewName)));
                results.Add(result);
            }
            if (results.TrueForAll(x => x.Equals(true)))
            {
                return datas;
            }
            //TODO throw custom exception
            return null;
        }
    }
}
