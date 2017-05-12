using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    /// <summary>
    /// Function model
    /// </summary>
    public class FunctionModel
    {
        /// <summary>
        /// Function code
        /// </summary>
        [StringLength(20, ErrorMessage = "Mã không vượt quá 20 ký tự")]
        public string Code { get; set; }

        /// <summary>
        /// Function's name
        /// </summary>
        [StringLength(50, ErrorMessage = "Tên không vượt quá 50 ký tự")]
        public string Name { get; set; }

        /// <summary>
        /// Action
        /// </summary>
        [StringLength(20, ErrorMessage = "Action không vượt quá 20 ký tự")]
        public string Action { get; set; }

        /// <summary>
        /// Controller
        /// </summary>
        [StringLength(20, ErrorMessage = "Controller không vượt quá 20 ký tự")]
        public string Controller { get; set; }

        /// <summary>
        /// Area
        /// </summary>
        [StringLength(20, ErrorMessage = "Area không vượt quá 20 ký tự")]
        public string Area { get; set; }

        /// <summary>
        /// Group name
        /// </summary>
        [StringLength(50, ErrorMessage = "Nhóm quyền không vượt quá 50 ký tự")]
        public string GroupName { get; set; }
    }
}
