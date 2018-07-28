Welcome to XCORE!!!
--------------------


The dead simple ASP.NET Core MVC Starter Project.


Create by: Ismail Hamzah / go2ismail@gmail.com



quick starter guide: (For CodeRush Developer Team)
***please follow the conventions because the generator use the conventions informations***

1. start by creating model 
- model without child relations, derived from INetcoreBasic
- model with child relations, derived from INetcoreMasterChild. only applied to master entity

*master child naming conventions: child entity name should be ended with "Line" and following by its master.
example:
SalesOrder			(master)
SalesOrderLine		(child)

Todo				(master)
TodoLine			(child)

2. create the MVC controller using scafolding by choosing MVC Controller with views using EF.
select the entity from the dropdownlist
select all the checkbox and leave the page layout box empty.
***naming conventions: controller name using the exactly same name with its entity name, following by "Controller"***

3. create API controller only for Child entity for using in Ajax/Jquery CRUD.
***we will generate the API format to suite the datatables.net***

4. add migration file by executing:
PM> add-migration yourmigrationname

5. execute the migration file to create the tables / database if not exist yet, by executing:
PM> update-database

6. run the application by pressing the play button
default username: super@admin.com
default password: 123456

7. your newly module will not appear yet in the module menu, please set its role by using User Role menu.

8. done.








Models Examples
===============

    public class Ticket : INetcoreBasic
    {
        public Ticket()
        {
            this.createdAt = DateTime.UtcNow;
        }

        [StringLength(38)]
        [Display(Name = "Ticket Id")]
        public string ticketId { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name = "Ticket Title")]
        public string ticketTitle { get; set; }

        [Display(Name = "Ticket Date")]
        public DateTime ticketDate { get; set; }

        [Display(Name = "Ticket Channel")]
        public TicketChannel ticketChannel { get; set; }

        [Display(Name = "Product Name")]
        public Product product { get; set; }

        [Display(Name = "Product Id")]
        [StringLength(38)]
        public string productId { get; set; }
    }

    public enum TicketChannel
    {
        Email = 1,
        Phone = 2,
        Web = 3
    }

    public class Product : INetcoreBasic
    {
        public Product()
        {
            this.createdAt = DateTime.UtcNow;
        }

        [StringLength(38)]
        [Display(Name = "Product Id")]
        public string productId { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name = "Product Name")]
        public string productName { get; set; }

        
    }

	public class Todo : INetcoreMasterChild
    {
        public Todo()
        {
            this.createdAt = DateTime.UtcNow;
        }

        [StringLength(38)]
        public string todoId { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Todo Name")]
        public string todoName { get; set; }
        [StringLength(100)]
        [Display(Name = "Todo Description")]
        public string description { get; set; }

        public List<TodoLine> todoLines { get; set; } = new List<TodoLine>();
    }

	public class TodoLine : INetcoreBasic
    {
        public TodoLine()
        {
            this.createdAt = DateTime.UtcNow;
        }

        [StringLength(38)]
        public string todoLineId { get; set; }
        [StringLength(50)]
        [Display(Name = "Todo Line Name")]
        public string todoLineName { get; set; }
        [StringLength(100)]
        [Display(Name = "Description")]
        public string description { get; set; }

        [Display(Name = "Todo")]
        [StringLength(38)]
        public string todoId { get; set; }
        [Display(Name = "Todo")]
        public Todo todo { get; set; }
    }

	public class Customer : INetcoreBasic
    {
        public Customer()
        {
            this.createdAt = DateTime.UtcNow;
        }

        [StringLength(38)]
        [Display(Name = "Customer Id")]
        public string customerId { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Customer Name")]
        public string customerName { get; set; }
    }