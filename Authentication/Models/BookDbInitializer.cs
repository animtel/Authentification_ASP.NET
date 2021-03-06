﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Authentication.Models
{
    public class BookDbInitializer : DropCreateDatabaseAlways<TestBD> // позволяет при каждом запуске приложения, заполнять бд заново
    {

        protected override void Seed(TestBD db)
        {


            db.Journales.Add(new Journal { Name = "Journa", Author = "Л. Толстой", Price = 220, Number = "1" });
            db.Journales.Add(new Journal { Name = "Some", Author = "Л. Толстой", Price = 123, Number = "5" });
            db.Journales.Add(new Journal { Name = "Intes", Author = "Л. Толстой", Price = 450, Number = "3" });
            db.Journales.Add(new Journal { Name = "Ret", Author = "Л. Толстой", Price = 120, Number = "8" });

            db.Brochures.Add(new Brochure {Id = 1, Name = "qwer", Color = "red", Theme = "ves", Price = 122});
            db.Brochures.Add(new Brochure { Id = 2, Name = "Brochure", Color = "green", Theme = "gt", Price = 213 });
            db.Brochures.Add(new Brochure { Id = 3, Name = "Int", Color = "blue", Theme = "asd", Price = 353 });
            db.Brochures.Add(new Brochure { Id = 4, Name = "Double", Color = "some", Theme = "fgh", Price = 123 });
            db.Brochures.Add(new Brochure { Id = 5, Name = "Decimal", Color = "ent", Theme = "hjk", Price = 865 });
            db.Brochures.Add(new Brochure { Id = 6, Name = "heh", Color = "jjj", Theme = "kl", Price = 435 });


            db.Books.Add(new Book { Name = "Отцы и дети", Author = "И. Тургенев", Price = 180 });
            db.Books.Add(new Book { Name = "Чайка", Author = "А. Чехов", Price = 150 });
            db.Books.Add(new Book { Name = "Some", Author = "А. Чехов", Price = 160 });
            db.Books.Add(new Book { Name = "Word", Author = "А. Чехов", Price = 170 });
            db.Books.Add(new Book { Name = "Project", Author = "А. Чехов", Price = 180 });
            db.Books.Add(new Book { Name = "Interest", Author = "А. Чехов", Price = 190 });
            db.Books.Add(new Book { Name = "Kendo", Author = "А. Чехов", Price = 200 });
            db.Books.Add(new Book { Name = "UI", Author = "А. Чехов", Price = 210 });
            db.Books.Add(new Book { Name = "loop", Author = "А. Чехов", Price = 220 });


            base.Seed(db);
        }
    }
}