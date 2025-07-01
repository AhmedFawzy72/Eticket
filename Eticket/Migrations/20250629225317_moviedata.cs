using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eticket.Migrations
{
    /// <inheritdoc />
    public partial class moviedata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Movies (Name, Description, Price, StartDate, EndtDate, CinemaId, CategoryId, Status) VALUES \r\n('Noel', 'Fusce consequat...', 89.99, '2025-06-01T10:00:00Z', '2025-07-01T10:00:00Z', 10, 5, 0),\r\n('Cly', 'Aenean fermentum...', 149.50, '2025-06-03T14:30:00Z', '2025-07-03T14:30:00Z', 10, 3, 1),\r\n('Christabel', 'Integer ac leo...', 120.75, '2025-06-05T18:00:00Z', '2025-07-05T18:00:00Z', 10, 4, 2),\r\n('Carolus', 'Curabitur in libero...', 199.00, '2025-06-07T16:45:00Z', '2025-07-07T16:45:00Z', 10, 4, 1),\r\n('Donnie', 'In sagittis dui...', 105.25, '2025-06-09T12:15:00Z', '2025-07-09T12:15:00Z', 10, 3, 0),\r\n('Freemon', 'Curabitur gravida nisi...', 82.40, '2025-06-11T20:00:00Z', '2025-07-11T20:00:00Z', 10, 5, 2),\r\n('Sadie', 'Cum sociis natoque...', 137.30, '2025-06-13T17:30:00Z', '2025-07-13T17:30:00Z', 10, 2, 1),\r\n('Enrique', 'Duis bibendum...', 95.90, '2025-06-15T11:00:00Z', '2025-07-15T11:00:00Z', 10, 3, 0),\r\n('Jacquelin', 'Aliquam quis turpis...', 123.45, '2025-06-17T15:20:00Z', '2025-07-17T15:20:00Z', 10, 2, 2),\r\n('Ilyssa', 'Cras non velit...', 111.10, '2025-06-18T09:00:00Z', '2025-07-18T09:00:00Z', 10, 1, 1),\r\n('Dale', 'Vestibulum quam sapien...', 130.60, '2025-06-20T13:15:00Z', '2025-07-20T13:15:00Z', 10, 2, 2),\r\n('Rianon', 'Fusce consequat...', 72.80, '2025-06-21T16:45:00Z', '2025-07-21T16:45:00Z', 10, 4, 0),\r\n('Duke', 'Morbi non lectus...', 140.25, '2025-06-23T19:30:00Z', '2025-07-23T19:30:00Z', 10, 3, 1),\r\n('Cora', 'In hac habitasse...', 152.95, '2025-06-24T10:30:00Z', '2025-07-24T10:30:00Z', 10, 5, 2),\r\n('Michell', 'Phasellus in felis...', 98.00, '2025-06-25T17:00:00Z', '2025-07-25T17:00:00Z', 10, 4, 0),\r\n('Geraldine', 'Vestibulum quam sapien...', 175.25, '2025-06-26T11:00:00Z', '2025-07-26T11:00:00Z', 10, 5, 1),\r\n('Nita', 'Aenean fermentum...', 89.00, '2025-06-27T12:30:00Z', '2025-07-27T12:30:00Z', 10, 2, 2),\r\n('Osbourn', 'Curabitur gravida nisi...', 117.70, '2025-06-28T14:00:00Z', '2025-07-28T14:00:00Z', 10, 2, 1),\r\n('Sergeant', 'Cum sociis natoque...', 135.90, '2025-06-29T16:00:00Z', '2025-07-29T16:00:00Z', 10, 4, 0),\r\n('Raff', 'Vestibulum ac est...', 149.99, '2025-06-30T18:00:00Z', '2025-07-30T18:00:00Z', 10, 1, 1),\r\n('Roley', 'Cras non velit...', 160.80, '2025-07-01T20:30:00Z', '2025-07-31T20:30:00Z', 10, 5, 2),\r\n('Erinna', 'Cras non velit...', 125.00, '2025-07-02T10:00:00Z', '2025-08-01T10:00:00Z', 10, 3, 1),\r\n('Desi', 'Nullam sit amet turpis...', 99.90, '2025-07-03T13:30:00Z', '2025-08-02T13:30:00Z', 10, 4, 0),\r\n('Ranice', 'Integer ac leo...', 132.40, '2025-07-04T15:00:00Z', '2025-08-03T15:00:00Z', 10, 4, 2),\r\n('Elmer', 'Aliquam quis turpis...', 106.30, '2025-07-05T17:30:00Z', '2025-08-04T17:30:00Z', 10, 2, 1),\r\n('Davis', 'In sagittis dui...', 142.70, '2025-07-06T10:45:00Z', '2025-08-05T10:45:00Z', 10, 4, 0),\r\n('Giffer', 'Integer ac leo...', 170.00, '2025-07-07T14:00:00Z', '2025-08-06T14:00:00Z', 10, 5, 2),\r\n('Kathrine', 'In hac habitasse...', 112.45, '2025-07-08T11:30:00Z', '2025-08-07T11:30:00Z', 10, 3, 1),\r\n('Wilmer', 'Curabitur in libero...', 102.99, '2025-07-09T16:30:00Z', '2025-08-08T16:30:00Z', 10, 2, 0),\r\n('Larisa', 'Duis bibendum...', 95.00, '2025-07-10T18:15:00Z', '2025-08-09T18:15:00Z', 10, 4, 2)");
        }
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("truncate table Movies");

        }
    }
}
