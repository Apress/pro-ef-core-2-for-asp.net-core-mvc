# Errata for *Pro Entity Framework Core 2 for ASP.NET Core MVC*


On **pages 45 & 46**:

The `CREATE TABLE` command in *Listing 3-14* includes a `ResponseId` column that is not used but that is referred to in the text. *Listing 3-14* should read:

    USE PartyInvites

    DROP TABLE IF EXISTS Preferences

    CREATE TABLE Preferences (
        Id bigint IDENTITY,
        Email nvarchar(max),
        NutAllergy bit,
        Teetotal bit
    )

The sentence immediately before *Table 3-10* should read:

 *Notice that the `Id` property of the row in the `Preferences` table matches 
the `Id` value for the row shown in Table 3-9.*

(Thanks to Andi Setiawan and Larry Usman for reporting this problem)

***
