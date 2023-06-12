using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.IO;

namespace MTW
{
    internal class ActionsWithPictures
    {
        public static string pathImages = @"C:\Users\vlad-\OneDrive\Рабочий стол\BibFond\BibFond\Images\booklab";

        public static byte[] ConvertImageToBinary(string iFile)
        {
            FileInfo fInfo = new FileInfo(iFile);
            long numBytes = fInfo.Length;
            FileStream fStream = new FileStream(iFile, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fStream);
            // конвертация изображения в байты
            byte[] imageData = br.ReadBytes((int)numBytes);
            return imageData;
        }

        public static void GetBase64ImageFromDb(int id)
        {
            if (File.Exists($"{pathImages}MTWImage_{id}.jpg")) return;
            List<byte[]> iScreen = new List<byte[]>(); // сделав запрос к БД мы получим множество строк в ответе, поэтому мы их сможем загнать в массив/List
            using (SqlConnection sqlConnection = new SqlConnection(@"data source=LAPTOP-GRBD40RP;initial catalog=MTW;integrated security=True"))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                //sqlCommand.CommandText = $"SELECT image FROM books WHERE id = {id}"; // наша запись в БД под id=1, поэтому в запросе "WHERE [id] = 1"
                SqlDataReader sqlReader = sqlCommand.ExecuteReader();
                byte[] iTrimByte = null;
                while (sqlReader.Read()) // считываем и вносим в лист результаты
                {
                    iTrimByte = (byte[])sqlReader["image"]; // читаем строки с изображениями
                    iScreen.Add(iTrimByte);
                }
                sqlConnection.Close();
            }
            // конвертируем бинарные данные в изображение
            byte[] imageData = iScreen[0]; // возвращает массив байт из БД. Так как у нас SQL вернёт одну запись и в ней хранится нужное нам изображение, то из листа берём единственное значение с индексом '0'
            MemoryStream ms = new MemoryStream(imageData);
            System.Drawing.Image newImage = System.Drawing.Image.FromStream(ms);
            // сохраняем изоражение на диск
            string imageName = @"" + pathImages + "MTWImage_" + id + ".jpg";
            newImage.Save(imageName, ImageFormat.Jpeg);
        }
    }
}