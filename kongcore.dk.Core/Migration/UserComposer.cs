using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.Composing;
using Umbraco.Core.Migrations;
using Umbraco.Core.Migrations.Upgrade;
using Umbraco.Core.Scoping;
using Umbraco.Core.Services;
using NPoco;
using Umbraco.Core.Persistence.DatabaseAnnotations;
using System.ComponentModel.DataAnnotations;
using System;

namespace Umbraco.Web.UI
{
    [RuntimeLevel(MinLevel = RuntimeLevel.Run)]
    public class MyUserComposer : ComponentComposer<MyUserComponent>
    {
    }

    public class MyUserComponent : IComponent
    {
        private IScopeProvider _scopeProvider;
        private IMigrationBuilder _migrationBuilder;
        private IKeyValueService _keyValueService;
        private ILogger _logger;

        public MyUserComponent(IScopeProvider scopeProvider, IMigrationBuilder migrationBuilder, IKeyValueService keyValueService, ILogger logger)
        {
            _scopeProvider = scopeProvider;
            _migrationBuilder = migrationBuilder;
            _keyValueService = keyValueService;
            _logger = logger;
        }

        public void Initialize()
        {
            // Create a migration plan for a specific project/feature
            // We can then track that latest migration state/step for this project/feature
            var migrationPlan = new MigrationPlan("AddMyUser");

            // This is the steps we need to take
            // Each step in the migration adds a unique value
            migrationPlan.From(string.Empty)
                .To<AddMyUserTable>("myuser-db");
            //migrationPlan.From("myuser-db")
            //    .To<AddMyUserTable>("myorder-db");
            //migrationPlan.From("myorder-db")
            //    .To<AddMyOrderTable>("myorder2-db");
            //migrationPlan.From("myuser-db")
            //    .To<AddMyOrderTable>("myorder2-db");
            //migrationPlan.From("myorder2-db")
            //    .To<AddMyOrderTable3>("myorder3-db");

            // Go and upgrade our site (Will check if it needs to do the work or not)
            // Based on the current/latest step
            var upgrader = new Upgrader(migrationPlan);
            upgrader.Execute(_scopeProvider, _migrationBuilder, _keyValueService, _logger);
        }

        public void Terminate()
        {
        }
    }

    public class AddMyUserTable : MigrationBase
    {
        public AddMyUserTable(IMigrationContext context) : base(context)
        {
        }

        public override void Migrate()
        {
            Logger.Debug<AddMyUserTable>("Running migration {MigrationStep}", "AddMyUserTable");

            // Lots of methods available in the MigrationBase class - discover with this.
            if (TableExists("MyUser") == false)
            {
                Create.Table<MyUserSchema>().Do();
            }
            else
            {
                Logger.Debug<AddMyUserTable>("The database table {DbTable} already exists, skipping", "MyUser");
            }
        }

        [TableName("MyUser")]
        [PrimaryKey("Id", AutoIncrement = true)]
        [ExplicitColumns]
        public class MyUserSchema
        {
            [PrimaryKeyColumn(AutoIncrement = true, IdentitySeed = 1)]
            [Column("Id")]
            public int Id { get; set; }

            [Column("Name")]
            public string Name { get; set; }

            [Column("Email")]
            public string Email { get; set; }
        }
    }

    //public class AddMyOrderTable : MigrationBase
    //{
    //    public AddMyOrderTable(IMigrationContext context) : base(context)
    //    {
    //    }

    //    public override void Migrate()
    //    {
    //        Logger.Debug<AddMyOrderTable>("Running migration {MigrationStep}", "AddMyOrderTable");

    //        // Lots of methods available in the MigrationBase class - discover with this.
    //        if (TableExists("MyOrder") == false)
    //        {
    //            //Create.Table<MyOrderSchema>().Do();
    //        }
    //        else
    //        {
    //            Logger.Debug<AddMyOrderTable>("The database table {DbTable} already exists, skipping", "MyOrder");
    //        }
    //    }

    //    [TableName("MyOrder")]
    //    [PrimaryKey("Id", AutoIncrement = true)]
    //    [ExplicitColumns]
    //    public class MyOrderSchema
    //    {

    //    }
    //}

    //public class AddMyOrderTable2 : MigrationBase
    //{
    //    public AddMyOrderTable2(IMigrationContext context) : base(context)
    //    {
    //    }

    //    public override void Migrate()
    //    {
    //        Logger.Debug<AddMyOrderTable2>("Running migration {MigrationStep}", "AddMyOrderTable");

    //        // Lots of methods available in the MigrationBase class - discover with this.
    //        if (TableExists("MyOrder") == false)
    //        {
    //            //Create.Table<MyOrderSchema>().Do();
    //        }
    //        else
    //        {
    //            Logger.Debug<AddMyOrderTable2>("The database table {DbTable} already exists, skipping", "MyOrder");
    //        }
    //    }

    //    [TableName("MyOrder")]
    //    [PrimaryKey("Id", AutoIncrement = true)]
    //    [ExplicitColumns]
    //    public class MyOrderSchema
    //    {

    //    }
    //}

    //public class AddMyOrderTable3 : MigrationBase
    //{
    //    public AddMyOrderTable3(IMigrationContext context) : base(context)
    //    {
    //    }

    //    public override void Migrate()
    //    {
    //        Logger.Debug<AddMyOrderTable3>("Running migration {MigrationStep}", "AddMyOrderTable");

    //        // Lots of methods available in the MigrationBase class - discover with this.
    //        if (TableExists("MyOrder") == false)
    //        {
    //            Create.Table<MyOrderSchema>().Do();
    //        }
    //        else
    //        {
    //            Logger.Debug<AddMyOrderTable3>("The database table {DbTable} already exists, skipping", "MyOrder");
    //        }
    //    }

    //    [TableName("MyOrder")]
    //    [PrimaryKey("Id", AutoIncrement = true)]
    //    [ExplicitColumns]
    //    public class MyOrderSchema
    //    {
    //        [PrimaryKeyColumn(AutoIncrement = true, IdentitySeed = 1)]
    //        [Column("Id")]
    //        public int Id { get; set; }

    //        [Column("Reference")]
    //        public string Reference { get; set; }
    //        [Column("Chargeid")]
    //        public string Chargeid { get; set; }
    //        [Column("Paymentid")]
    //        public string Paymentid { get; set; }
    //        [Column("Mask")]
    //        public string Mask { get; set; }

    //        [Column("Date")]
    //        public DateTime Date { get; set; }

    //        [Column("ShopName")]
    //        public string ShopName { get; set; }
    //        [Column("ShopEmail")]
    //        public string ShopEmail { get; set; }
    //        [Column("ShopPhone")]
    //        public int ShopPhone { get; set; }
    //        [Column("ShopAddress")]
    //        public string ShopAddress { get; set; }

    //        [Column("ProductName")]
    //        public string ProductName { get; set; }
    //        [Column("ProductAmount")]
    //        public int ProductAmount { get; set; }
    //        [Column("ProductQty")]
    //        public int ProductQty { get; set; }

    //        [Column("CustId")]
    //        public string CustId { get; set; }
    //        [Column("CustEmail")]
    //        public string CustEmail { get; set; }
    //        [Column("CustName")]
    //        public string CustName { get; set; }

    //        [Column("Sent")]
    //        public bool Sent { get; set; }
    //        [Column("Done")]
    //        public bool Done { get; set; }
    //        [Column("Receipt")]
    //        public bool Receipt { get; set; }
    //        [Column("Refund")]
    //        public bool Refund { get; set; }
    //        [Column("Paid")]
    //        public bool Paid { get; set; }

    //        [Column("ShippingPrice")]
    //        public float ShippingPrice { get; set; }
    //    }
    //}
}