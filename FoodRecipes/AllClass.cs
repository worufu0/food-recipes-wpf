using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace FoodRecipes
{
	// Bước
	public class Step
	{
		// Nội dung.
		public string content { get; set; }
		// Hình ảnh minh họa.
		public string guideImage { get; set; }
		// Loại của bước.
		// 1: Chuẩn bị.
		// 2: Chế biến.
		// 3: Thành phẩm.
		public int stepType { get; set; }
	}

	// Món ăn
	public class Recipe
	{
		// Tên của món ăn.
		public string foodName { get; set; }
		// Mô tả.
		public string description { get; set; }
		// Hình ảnh.
		public string image { get; set; }
		// Video.
		public string video { get; set; }
		// Ưa thích.
		public bool favorite { get; set; }
		// Loại của món ăn.
		// 1: Đồ uống
		// 2: Khai vị
		// 3: Món chính
		// 4: Tráng miệng
		public int foodType { get; set; }
		// Các bước thực hiện.
		public List<Step> steps = new List<Step>();
	}

	// Danh sách các mẹo
	public class TipList
	{
		public List<string> tips = new List<string>() { };

		/// <summary>
		/// Chọn ra mẹo ngẫu nhiên từ mảng tips.
		/// </summary>
		/// <returns>Mẹo</returns>
		public string getTip()
		{
			Random rng = new Random();
			string tip = tips[rng.Next(tips.Count())];
			return tip;
		}
	}

	// Danh sách món ăn.
	public class RecipeList
	{
		// Các công thức có sẵn.
		public List<Recipe> availableRecipes = new List<Recipe>();
		// Các công thức được thêm vào.
		public List<Recipe> addedRecipes = new List<Recipe>();
	}

	// Toàn bộ dữ liệu người dùng.
	public class Data
	{
		// Danh sách món ăn.
		public RecipeList recipeList = new RecipeList();
		// Danh sách mẹo vặt.
		public TipList tipList = new TipList();

		/// <summary>
		/// Lưu dữ liệu người dùng vào file
		/// </summary>
		/// <param name="obj">Dữ liệu cần lưu</param>
		/// <param name="fileName">Đường dẫn file</param>
		public void saveData(object obj, string fileName)
		{
			XmlSerializer serializer = new XmlSerializer(obj.GetType());
			StreamWriter writer = new StreamWriter(fileName);

			serializer.Serialize(writer, obj);
			writer.Close();
		}

		/// <summary>
		/// Đọc dữ liệu người dùng từ file
		/// </summary>
		/// <param name="data">Kiểu của dữ liệu</param>
		/// <param name="fileName">Đường dẫn file</param>
		public Data loadData(Data data, string fileName)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(Data));
			StreamReader reader = new StreamReader(fileName);

			data = (Data)serializer.Deserialize(reader);
			reader.Close();
			return data;
		}
	}

	// Toàn bộ dữ liệu cấu hình
	public class Config
	{
		// Vô hiệu hóa splash screen.
		public bool splashScreenDisabled { get; set; }

		/// <summary>
		/// Lưu dữ liệu cấu hình vào file
		/// </summary>
		/// <param name="obj">Dữ liệu cần lưu</param>
		/// <param name="fileName">Đường dẫn file</param>
		public void saveConfig(object obj, string fileName)
		{
			XmlSerializer serializer = new XmlSerializer(obj.GetType());
			StreamWriter writer = new StreamWriter(fileName);

			serializer.Serialize(writer, obj);
			writer.Close();
		}

		/// <summary>
		/// Đọc dữ liệu cấu hình từ file
		/// </summary>
		/// <param name="data">Kiểu của dữ liệu</param>
		/// <param name="fileName">Đường dẫn file</param>
		public Config loadConfig(Config config, string fileName)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(Config));
			StreamReader reader = new StreamReader(fileName);

			config = (Config)serializer.Deserialize(reader);
			reader.Close();
			return config;
		}
	}
}