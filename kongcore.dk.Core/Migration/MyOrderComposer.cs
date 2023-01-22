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
    [ComposeAfter(typeof(MyUserComposer))]
    [RuntimeLevel(MinLevel = RuntimeLevel.Run)]
    public class MyOrderComposer : ComponentComposer<MyOrderComponent>
    {
    }

    public class MyOrderComponent : IComponent
    {
        private IScopeProvider _scopeProvider;
        private IMigrationBuilder _migrationBuilder;
        private IKeyValueService _keyValueService;
        private ILogger _logger;

        public MyOrderComponent(IScopeProvider scopeProvider, IMigrationBuilder migrationBuilder, IKeyValueService keyValueService, ILogger logger)
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
            var migrationPlan = new MigrationPlan("AddMyOrder");

            // This is the steps we need to take
            // Each step in the migration adds a unique value
            migrationPlan.From(string.Empty)
                .To<AddMyOrderTable>("myorder-db");
            migrationPlan.From("myorder-db")
                .To<UpdateOrderNullable>("myorder2-db");
            migrationPlan.From("myorder2-db")
                .To<UpdateOrderNotNullable>("myorder3-db");
            migrationPlan.From("myorder3-db")
                .To<UpdateOrderChargeIDNullable>("myorder4-db");
            migrationPlan.From("myorder4-db")
                .To<UpdateOrderCustIDNullable>("myorder5-db");

            migrationPlan.From("myorder5-db")
                .To<UpdateOrderCustIDNullable2>("myorder6-db");
            migrationPlan.From("myorder6-db")
                .To<UpdateOrderChargeIDNullable2>("myorder7-db");

            // Go and upgrade our site (Will check if it needs to do the work or not)
            // Based on the current/latest step
            var upgrader = new Upgrader(migrationPlan);
            upgrader.Execute(_scopeProvider, _migrationBuilder, _keyValueService, _logger);
        }

        public void Terminate()
        {
        }
    }

    public class AddMyOrderTable : MigrationBase
    {
        public AddMyOrderTable(IMigrationContext context) : base(context)
        {
        }

        public override void Migrate()
        {
            Logger.Debug<AddMyOrderTable>("Running migration {MigrationStep}", "AddMyOrderTable");

            // Lots of methods available in the MigrationBase class - discover with this.
            if (TableExists("MyOrder") == false)
            {
                Create.Table<MyOrderSchema>().Do();
            }
            else
            {
                Logger.Debug<AddMyOrderTable>("The database table {DbTable} already exists, skipping", "MyOrder");
            }
        }

        [TableName("MyOrder")]
        [PrimaryKey("Id", AutoIncrement = true)]
        [ExplicitColumns]
        public class MyOrderSchema
        {
            [PrimaryKeyColumn(AutoIncrement = true, IdentitySeed = 1)]
            [Column("Id")]
            public int Id { get; set; }

            [Column("Reference")]
            public string Reference { get; set; }
            [Column("Chargeid")]
            public string Chargeid { get; set; }
            [Column("Paymentid")]
            public string Paymentid { get; set; }
            [Column("Mask")]
            public string Mask { get; set; }

            [Column("Date")]
            public DateTime Date { get; set; }

            [Column("ShopName")]
            public string ShopName { get; set; }
            [Column("ShopEmail")]
            public string ShopEmail { get; set; }
            [Column("ShopPhone")]
            public int ShopPhone { get; set; }
            [Column("ShopAddress")]
            public string ShopAddress { get; set; }

            [Column("ProductName")]
            public string ProductName { get; set; }
            [Column("ProductAmount")]
            public int ProductAmount { get; set; }
            [Column("ProductQty")]
            public int ProductQty { get; set; }

            [Column("CustId")]
            public string CustId { get; set; }
            [Column("CustEmail")]
            public string CustEmail { get; set; }
            [Column("CustName")]
            public string CustName { get; set; }

            [Column("Sent")]
            public bool Sent { get; set; }
            [Column("Done")]
            public bool Done { get; set; }
            [Column("Receipt")]
            public bool Receipt { get; set; }
            [Column("Refund")]
            public bool Refund { get; set; }
            [Column("Paid")]
            public bool Paid { get; set; }

            [Column("ShippingPrice")]
            public float ShippingPrice { get; set; }
        }
    }

    public class UpdateOrderNullable : MigrationBase
    {
        public UpdateOrderNullable(IMigrationContext context) : base(context)
        {
        }

        public override void Migrate()
        {
            Logger.Debug<UpdateOrderNullable>("Running migration {MigrationStep}", "UpdateOrderNullable");

            // Lots of methods available in the MigrationBase class - discover with this.
            if (TableExists("MyOrder"))
            {
                this.
                Execute.Sql(
                "ALTER TABLE MyOrder ALTER COLUMN CustId nvarchar NULL"
                ).Do();
            }
            else
            {
                Logger.Debug<UpdateOrderNullable>("The database table {DbTable} already exists, skipping", "UpdateOrderNullable");
            }
        }
    }

    public class UpdateOrderNotNullable : MigrationBase
    {
        public UpdateOrderNotNullable(IMigrationContext context) : base(context)
        {
        }

        public override void Migrate()
        {
            Logger.Debug<UpdateOrderNotNullable>("Running migration {MigrationStep}", "UpdateOrderNotNullable");

            // Lots of methods available in the MigrationBase class - discover with this.
            if (TableExists("MyOrder"))
            {
                this.
                Execute.Sql(
                "ALTER TABLE MyOrder ALTER COLUMN CustId nvarchar NOT NULL"
                ).Do();
            }
            else
            {
                Logger.Debug<UpdateOrderNotNullable>("The database table {DbTable} already exists, skipping", "UpdateOrderNotNullable");
            }
        }
    }

    public class UpdateOrderChargeIDNullable : MigrationBase
    {
        public UpdateOrderChargeIDNullable(IMigrationContext context) : base(context)
        {
        }

        public override void Migrate()
        {
            Logger.Debug<UpdateOrderNotNullable>("Running migration {MigrationStep}", "UpdateOrderChargeIDNullable ");

            // Lots of methods available in the MigrationBase class - discover with this.
            if (TableExists("MyOrder"))
            {
                this.
                Execute.Sql(
                "ALTER TABLE MyOrder ALTER COLUMN Chargeid nvarchar NULL"
                ).Do();
            }
            else
            {
                Logger.Debug<UpdateOrderNotNullable>("The database table {DbTable} already exists, skipping", "UpdateOrderNotNullable");
            }
        }
    }

    public class UpdateOrderCustIDNullable : MigrationBase
    {
        public UpdateOrderCustIDNullable(IMigrationContext context) : base(context)
        {
        }

        public override void Migrate()
        {
            Logger.Debug<UpdateOrderCustIDNullable>("Running migration {MigrationStep}", "UpdateOrderCustIDNullable");

            // Lots of methods available in the MigrationBase class - discover with this.
            if (TableExists("MyOrder"))
            {
                this.
                Execute.Sql(
                "ALTER TABLE MyOrder ALTER COLUMN CustId nvarchar NULL"
                ).Do();
            }
            else
            {
                Logger.Debug<UpdateOrderCustIDNullable>("The database table {DbTable} already exists, skipping", "UpdateOrderCustIDNullable");
            }
        }
    }

    public class UpdateOrderCustIDNullable2 : MigrationBase
    {
        public UpdateOrderCustIDNullable2(IMigrationContext context) : base(context)
        {
        }

        public override void Migrate()
        {
            Logger.Debug<UpdateOrderCustIDNullable2>("Running migration {MigrationStep}", "UpdateOrderCustIDNullable2");

            // Lots of methods available in the MigrationBase class - discover with this.
            if (TableExists("MyOrder"))
            {
                this.
                Execute.Sql(
                "ALTER TABLE MyOrder ALTER COLUMN CustId nvarchar(255) NULL"
                ).Do();
            }
            else
            {
                Logger.Debug<UpdateOrderCustIDNullable2>("The database table {DbTable} already exists, skipping", "UpdateOrderCustIDNullable2");
            }
        }
    }

    public class UpdateOrderChargeIDNullable2 : MigrationBase
    {
        public UpdateOrderChargeIDNullable2(IMigrationContext context) : base(context)
        {
        }

        public override void Migrate()
        {
            Logger.Debug<UpdateOrderChargeIDNullable2>("Running migration {MigrationStep}", "UpdateOrderChargeIDNullable2");

            // Lots of methods available in the MigrationBase class - discover with this.
            if (TableExists("MyOrder"))
            {
                this.
                Execute.Sql(
                "ALTER TABLE MyOrder ALTER COLUMN Chargeid nvarchar(255) NULL"
                ).Do();
            }
            else
            {
                Logger.Debug<UpdateOrderChargeIDNullable2>("The database table {DbTable} already exists, skipping", "UpdateOrderChargeIDNullable2");
            }
        }
    }
}