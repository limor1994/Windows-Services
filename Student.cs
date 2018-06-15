
namespace ImageServiceWeb.Models
{
    public class Student
    {
        private string student_firstname;
        private string student_lastname;
        private string student_id;

       

        /// <summary>
        /// Getter setter for students id
        /// </summary>
        public string ID
        {
            get { return student_id; }
            set
            {
                student_id = value;

            }
        }


        /// <summary>
        /// Getter setter for firstname
        /// </summary>
        public string FirstName
        {
            get { return student_firstname; }
            set
            {
                student_firstname = value;

            }
        }

        /// <summary>
        /// Getter setter for students lastname
        /// </summary>
        public string LastName
        {
            get { return student_lastname; }
            set
            {
                student_lastname = value;

            }
        }
    }
}