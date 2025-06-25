using App.DAL.DTO;
using Sector = App.Domain.Sector;

namespace WebApp.Helpers;

public static class FileHelper
{
    public static string BasePath = Environment
                                        .GetFolderPath(System.Environment.SpecialFolder.UserProfile)
                                    + Path.DirectorySeparatorChar + "Github" + Path.DirectorySeparatorChar +
                                    "net-form" + Path.DirectorySeparatorChar + "net-form" +
                                    Path.DirectorySeparatorChar + "db" + Path.DirectorySeparatorChar;
    
}