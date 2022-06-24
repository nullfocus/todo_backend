namespace TodoBackend{
    public class Todo{
        public int id{
            get; set;
        }

        public String name{
            get; set;
        }

        public DateTime? created{
            get; set;
        }

        public DateTime? completed{
            get; set;
        }

        public int priority{
            get; set;
        }

        public int parent_id{
            get; set;
        }

        public int user_id{
            get; set;
        }
    }
}