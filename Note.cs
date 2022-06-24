namespace TodoAppBackend.Controller
{
    public class Note
    {
        public int id{
            get; set;
        }

        public String title{
            get; set;
        }
        public String content{
            get; set;
        }

        public DateTime? created{
            get; set;
        }
        
        
    }
}