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
On **page 147**:

The view that presents the pagination controls applies the Bootstrap class to the Previous and Next buttons so they appear disabled but leaves the buttons enabled, such that clicking these buttons when viewing the first or last data page produces an error. *Listing 8-11* should read:

    <form id="pageform" method="get" class="form-inline d-inline-block">

        <button name="options.currentPage" value="@(Model.CurrentPage -1)"
                class="btn btn-outline-primary @(!Model.HasPreviousPage ? "disabled" : "")"
                disabled="@(!Model.HasPreviousPage)"
                type="submit">
            Previous
        </button>

        @for (int i = 1; i <= 3 && i <= Model.TotalPages; i++) {
            <button name="options.currentPage" value="@i" type="submit"
                    class="btn btn-outline-primary @(Model.CurrentPage == i ? "active" : "")">
                @i
            </button>
        }
        @if (Model.CurrentPage > 3 && Model.TotalPages - Model.CurrentPage >= 3) {
            @:...
            <button class="btn btn-outline-primary active">@Model.CurrentPage</button>
        }
        @if (Model.TotalPages > 3) {
            @:...
            @for (int i = Math.Max(4, Model.TotalPages - 2);
                                    i <= Model.TotalPages; i++) {
                <button name="options.currentPage" value="@i" type="submit"
                        class="btn btn-outline-primary
                    @(Model.CurrentPage == i ? "active" : "")">
                    @i
                </button>
            }
        }
        <button name="options.currentPage" value="@(Model.CurrentPage +1)" type="submit"
                class="btn btn-outline-primary @(!Model.HasNextPage? "disabled" : "")"
                disabled="@(!Model.HasNextPage)">
            Next
        </button>

        <select name="options.pageSize" class="form-control ml-1 mr-1">
            @foreach (int val in new int[] { 10, 25, 50, 100 }) {
                <option value="@val" selected="@(Model.PageSize == val)">@val</option>
            }
        </select>
        <input type="hidden" name="options.currentPage" value="1" />
        <button type="submit" class="btn btn-secondary">Change Page Size</button>
    </form>

This view adds the `disabled` attribute to the `Previous` and `Next` elements.

(Thanks to Jean-Michel Borel for reporting this problem)