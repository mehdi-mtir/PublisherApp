﻿using System.ComponentModel.DataAnnotations.Schema;

namespace PublisherDomain
{
    public class Book
    {
        public int BookId { get; set; }
        //[Column("MainTitle")]
        public string Title { get; set; }
        public DateOnly PublishDate { get; set; }
        public decimal BasePrice { get; set; }
        public Author Author { get; set; }
        public int AuthorId { get; set; }
        public Cover Cover { get; set; }


        public override string ToString()
        {
            return $"Livre : {Title} publié le {PublishDate.ToString()}";
        }
    }
}
