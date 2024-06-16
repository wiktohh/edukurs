using Domain.Entities;

namespace Edukurs.Application.Tests;

public class Helper
{
    public static User GetUser()
    {
        return new User(Guid.NewGuid(),"string2@gmail.com","test", "test","Password","Student");
    }
    
    public static User GetTeacher()
    {
        return new User(Guid.NewGuid(),"string2@gmail.com","test", "test","Password","Teacher");
    }
    
    public static User GetAdmin()
    {
        return new User(Guid.NewGuid(),"string2@gmail.com","test", "test","Password","Admin");
    }
}