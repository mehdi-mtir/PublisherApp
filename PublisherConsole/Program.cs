using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;

/*using (var context = new PubContext())
{
    context.Database.EnsureCreated();
}*/

using var context = new PubContext();

//AddAuthor("Bruce", "Alexander");
//AddAuthor("Michael", "Alexander");
//AddAuthor("Bob", "Anderson");
//AddAuthor("James", "Anderson");
//GetAuthors();
//GetSortedAuthors();
//GetAuthorsWithPagination(3);
//AddAuthorWithBooks();
//AddBookForExistingAuthor();
GetAuthorsWithBooks();


void AddAuthorWithBooks()
{
    var author = new Author { FirstName = "Julie", LastName = "Lerman" };
    author.Books.Add(new Book { Title = "Programming Entity Framework", BasePrice = 20.50m, PublishDate = new DateOnly(2016, 1, 1) });
    author.Books.Add(new Book { Title = "Programming Entity Framework: Code First", BasePrice = 15.40m, PublishDate = new DateOnly(2019, 1, 1) });
    author.Books.Add(new Book { Title = "Programming Entity Framework: DBContext", BasePrice = 18.40m, PublishDate = new DateOnly(2020, 1, 1) });
    //using var context = new PubContext();
    context.Authors.Add(author);
    context.SaveChanges();
}

void AddAuthor(string firstName, string lastName)
{
    var author = new Author { FirstName = firstName, LastName = lastName };
    //using var context = new PubContext();
    context.Authors.Add(author);
    context.SaveChanges();
}


void AddBookWithoutAuthor()
{
    var book = new Book { Title = "Smarter Faster Better", BasePrice = 14.50m, PublishDate = new DateOnly(2016, 1, 1) };

    //using var context = new PubContext();
    context.Books.Add(book);
    context.SaveChanges();
}

//UpdateAuthorName("Anderson", "Andersen");
//UpdateAuthorNameWithoutTracking("Adams", "Adam");
//DeleteAuthorById(5);





void AddBookForExistingAuthor()
{
    var book = new Book { Title = "Smarter Faster Better", BasePrice = 14.50m, PublishDate = new DateOnly(2016, 1, 1), AuthorId = 2 };

    //using var context = new PubContext();
    context.Books.Add(book);
    context.SaveChanges();
}

void UpdateAuthorName(string authorName, string newAuthorName)
{
    var authorsToUpdate = context.Authors.Where(a => a.LastName == authorName).ToList();
    foreach (var author in authorsToUpdate)
    {
        author.LastName = newAuthorName;
    }
    Console.WriteLine("Avant l'appel de la méthode DetectChange() : \n" + context.ChangeTracker.DebugView.ShortView);
    context.ChangeTracker.DetectChanges();
    Console.WriteLine("Après l'appel de la méthode DetectChange() : \n" + context.ChangeTracker.DebugView.ShortView);
    context.SaveChanges();
    Console.WriteLine("Après l'appel de la méthode SaveChanges() : \n" + context.ChangeTracker.DebugView.ShortView);
}

void UpdateAuthorNameWithoutTracking(string authorName, string newAuthorName)
{
    var authorsToUpdate = context.Authors.Where(a => a.LastName == authorName).ToList();


    foreach (var author in authorsToUpdate)
    {
        author.LastName = newAuthorName;
        //context.Update(author);
    }
    Console.WriteLine("Avant l'appel de la méthode DetectChange() : \n" + context.ChangeTracker.DebugView.ShortView);
    context.ChangeTracker.DetectChanges();
    Console.WriteLine("Après l'appel de la méthode DetectChange() : \n" + context.ChangeTracker.DebugView.ShortView);
    context.UpdateRange(authorsToUpdate);
    Console.WriteLine("Après l'appel de la méthode UpdateRange() : \n" + context.ChangeTracker.DebugView.ShortView);
    context.SaveChanges();
    Console.WriteLine("Après l'appel de la méthode SaveChanges() : \n" + context.ChangeTracker.DebugView.ShortView);
}

void DeleteAuthorById(int id)
{
    var author = context.Authors.Find(id);
    if(author != null)
    {
        context.Authors.Remove(author);
        Console.WriteLine("Avant l'appel de la méthode DetectChange() : \n" + context.ChangeTracker.DebugView.ShortView);
        context.ChangeTracker.DetectChanges();
        Console.WriteLine("Après l'appel de la méthode DetectChange() : \n" + context.ChangeTracker.DebugView.ShortView);
        context.SaveChanges();
        Console.WriteLine("Après l'appel de la méthode SaveChanges() : \n" + context.ChangeTracker.DebugView.ShortView);
        Console.WriteLine($"Auteur supprimé : {author}");
    }
    else
    {
        Console.WriteLine($"L'auteur ayant l'id {id} n'a pas été trouvé!");
    }
}

void GetAuthors()
{
    //using var context = new PubContext();
    var authors = context.Authors.ToList();
    foreach (var item in authors)
    {
        Console.WriteLine($"Auteur {item.AuthorId} : {item.FirstName} {item.LastName}");
    }
}

void GetAuthorsWithBooks()
{
    //using var context = new PubContext();
    var authorsWithBooks = context.Authors.Include(a => a.Books).ToList();
    foreach (var author in authorsWithBooks)
    {
        Console.WriteLine(author);
        foreach (var book in author.Books)
        {
            Console.WriteLine($"\t\t{book}");
        }
    }
}

void GetSortedAuthors()
{
   // using var context = new PubContext();
    var sortedAuthors = context.Authors.OrderBy(a=>a.LastName).ThenBy(a=>a.FirstName).ToList();
    foreach (var item in sortedAuthors)
    {
        Console.WriteLine(item);
    }

}

void GetAuthorsWithPagination(int numberPerPage)
{
    //using var context = new PubContext();
    int numberOfAuthors = context.Authors.Count();

    int numberOfAuthorsPerPage = (numberPerPage <= 0)?1: numberPerPage;

    for (int i = 0; i < Math.Ceiling( (decimal)numberOfAuthors / numberOfAuthorsPerPage ); i++)
    {
        var authors = context.Authors.Skip(numberOfAuthorsPerPage * i).Take(numberPerPage).ToList();
        Console.WriteLine("**** Page : " + (i + 1) + "*****");
        foreach (var author in authors)
        {
            Console.WriteLine(author);
        }
    

}}