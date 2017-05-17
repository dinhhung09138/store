using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public class DatabaseMessage
    {
        public const string LIST_SUCCESS = "Đọc dữ liệu thành công";
        public const string LIST_ERROR = "Lỗi hệ thống! Không thể đọc được dữ liệu";
        public const string ITEM_SUCCESS = "Đọc dữ liệu thành công";
        public const string ITEM_ERROR = "Lỗi hệ thống! Không thể đọc được dữ liệu";
        public const string SAVE_SUCCESS = "Thông tin đã lưu";
        public const string SAVE_ERROR = "Lỗi hệ thống! Không thể lưu dữ liệu";
        public const string DELETE_SUCCESS = "Đối tượng đã được xóa";
        public const string DELETE_ERROR = "Lỗi hệ thống! Không thể xóa dữ liệu";
    }
}
