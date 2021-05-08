using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Input;

namespace FoodRecipes
{
	//Toàn cục
	public class Global
	{
		//Dữ liệu
		public static Data data = new Data();
		//Chỉ mục
		public static int idTransfer { get; set; }
	}

	//Thông tin phân trang
	public class PageInfomation
	{
		//Trang hiện tại
		public static int currentPage { get; set; }
		//Kích thước trang
		public static int pageSize { get; set; }
		//Số lượng dữ liệu
		public static int itemCount { get; set; }
		//Số trang tối đa
        public static int total()
        {
			int result;
            if (itemCount % pageSize == 0)
            {
                result = itemCount / pageSize;
            }
            else
            {
				result = itemCount / pageSize + 1;
            }
			return result;
        }
    }

	// Bước
	public class Step
	{
		// Nội dung.
		public string content { get; set; }
		// Hình ảnh minh họa.
		public string guideImage { get; set; }
    }

	// Món ăn
	public class Recipe
	{
		//Số thứ tự của công thức
		public int foodID { get; set; }
		// Tên của món ăn.
		public string foodName { get; set; }
		// Mô tả.
		public string description { get; set; }
		// Hình ảnh.
		public string image { get; set; }
		// Video.
		public string video { get; set; }
		// Bảo vệ
		public bool beProtected { get; set; }
		// Ưa thích.
		public bool favorite { get; set; }
		// Loại của món ăn.
		public string foodType { get; set; }
		//Thời gian
		public string time { get; set; }
		// Các bước thực hiện.
		public List<Step> steps { get; set; }

		/// <summary>
		/// Tạo ra danh sách các bước
		/// </summary>
		/// <returns>Danh sách các bước</returns>
		public List<string> getStepNames()
		{
			List<string> result = new List<string>();
			result.Add("Chuẩn bị");
			for (int i = 1; i <= steps.Count - 2; i++)
            {
				result.Add($"Bước {i}");
			}
			result.Add("Thành phẩm");
			return result;
		}

		/// <summary>
		/// Tạo ra danh sách hình ảnh
		/// </summary>
		/// <returns>Danh sách hình ảnh</returns>
		public List<string> getThumbnails(int selectedIndex)
		{
			List<string> result = new List<string>();
			if (selectedIndex == 0)
            {
				result.Add(steps[steps.Count - 1].guideImage);
				for (int i = selectedIndex; i < selectedIndex + 2; i++)
				{
					result.Add(steps[i].guideImage);
				}
			}
			else if (selectedIndex == steps.Count - 1)
            {
				for (int i = selectedIndex - 1; i < selectedIndex + 1; i++)
				{
					result.Add(steps[i].guideImage);
				}
				result.Add(steps[0].guideImage);
			}
			else
            {
				for (int i = selectedIndex - 1; i < selectedIndex + 2; i++)
				{
					result.Add(steps[i].guideImage);
				}
			}
			return result;
		}
	}

	// Toàn bộ dữ liệu chương trình.
	public class Data
	{
		// Vô hiệu hóa splash screen.
		public bool splashScreenDisabled { get; set; }
		// Danh sách món ăn.
		public List<Recipe> recipes { get; set; }
		// Danh sách mẹo vặt.
		public List<string> tips { get; set; }

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

		/// <summary>
		/// Tìm ID hợp lệ
		/// </summary>
		/// <returns>ID hợp lệ</returns>
		public int searchValidID()
		{
			int result = 0;
			List<int> foodIDs = new List<int>();
			foreach (Recipe recipe in recipes)
            {
				foodIDs.Add(recipe.foodID);
			}
			for (int i = 0; i < int.MaxValue; i++)
			{
				if (!foodIDs.Contains(i))
                {
					result = i;
					break;
                }
            }
			return result;
		}

		/// <summary>
		/// Xác định công thức theo ID
		/// </summary>
		/// <param name="foodID">ID</param>
		/// <returns>Công thức</returns>
		public Recipe recipeIdentification(int foodID)
        {
			Recipe result = new Recipe();
			foreach (Recipe recipe in recipes)
            {
				if (foodID == recipe.foodID)
                {
					result = recipe;
				}
            }
			return result;
		}

		/// <summary>
		/// Tạo ra danh sách phân loại
		/// </summary>
		/// <returns>Danh sách phân loại</returns>
		public List<string> recipesClassify()
        {
			List<string> result = new List<string>();
			foreach (Recipe recipe in recipes)
            {
				if (!result.Contains(recipe.foodType))
                {
					result.Add(recipe.foodType);
                }
            }
			return result;
        }

		/// <summary>
		/// Lưu dữ liệu vào file
		/// </summary>
		/// <param name="obj">Dữ liệu cần lưu</param>
		/// <param name="fileName">Đường dẫn file</param>
		public void save(object obj, string fileName)
		{
			XmlSerializer serializer = new XmlSerializer(obj.GetType());
			StreamWriter writer = new StreamWriter(fileName);

			serializer.Serialize(writer, obj);
			writer.Close();
		}

		/// <summary>
		/// Đọc dữ liệu từ file
		/// </summary>
		/// <param name="data">Kiểu của dữ liệu</param>
		/// <param name="fileName">Đường dẫn file</param>
		/// <returns>Dữ liệu chương trinh</returns>
		public Data load(Data data, string fileName)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(Data));
			StreamReader reader = new StreamReader(fileName);

			data = (Data)serializer.Deserialize(reader);
			reader.Close();
			return data;
		}
	}
}