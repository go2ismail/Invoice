using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using netcore.Data;
using netcore.Models.Invoice;

namespace netcore.Controllers.Api
{

    [Produces("application/json")]
    [Route("api/CustomerInvoiceLine")]
    public class CustomerInvoiceLineController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerInvoiceLineController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CustomerInvoiceLine
        [HttpGet]
        [Authorize]
        public IActionResult GetCustomerInvoiceLine(string masterid)
        {
            return Json(new { data = _context.CustomerInvoiceLine.Include(x => x.item).Where(x => x.customerInvoiceId.Equals(masterid)).ToList() });
        }

        // POST: api/CustomerInvoiceLine
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostCustomerInvoiceLine([FromBody] CustomerInvoiceLine customerInvoiceLine)
        {
            Item item = await _context.Item.Include(x => x.tax).SingleOrDefaultAsync(x => x.itemId.Equals(customerInvoiceLine.itemId));
            customerInvoiceLine.taxId = item.taxId;

         

            if (customerInvoiceLine.customerInvoiceLineId == string.Empty)
            {
                customerInvoiceLine.customerInvoiceLineId = Guid.NewGuid().ToString();
               
                if (customerInvoiceLine.price.Equals(0m)) customerInvoiceLine.price = item.price;
                customerInvoiceLine.amount = (decimal)customerInvoiceLine.quantity * customerInvoiceLine.price;
                customerInvoiceLine.taxAmount = (decimal)(item.tax.taxRate / 100.0) * customerInvoiceLine.amount;
                customerInvoiceLine.totalAmount = customerInvoiceLine.amount + customerInvoiceLine.taxAmount;
                _context.CustomerInvoiceLine.Add(customerInvoiceLine);

                CustomerInvoice ci = await _context.CustomerInvoice.Include(x => x.customerInvoiceLine).SingleOrDefaultAsync(x => x.customerInvoiceId.Equals(customerInvoiceLine.customerInvoiceId));
                ci.subTotal = ci.customerInvoiceLine.Sum(x => x.amount);
                ci.taxAmount = ci.customerInvoiceLine.Sum(x => x.taxAmount);
                ci.grandTotal = ci.subTotal + ci.taxAmount - ci.discount + ci.shipping;
                _context.CustomerInvoice.Update(ci);

                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Add new data success." });
            }
            else
            {
                
                if (customerInvoiceLine.price.Equals(0m)) customerInvoiceLine.price = item.price;
                customerInvoiceLine.amount = (decimal)customerInvoiceLine.quantity * customerInvoiceLine.price;
                customerInvoiceLine.taxAmount = (decimal)(item.tax.taxRate / 100.0) * customerInvoiceLine.amount;
                customerInvoiceLine.totalAmount = customerInvoiceLine.amount + customerInvoiceLine.taxAmount;
                _context.Update(customerInvoiceLine);

                CustomerInvoice ci = await _context.CustomerInvoice.Include(x => x.customerInvoiceLine).SingleOrDefaultAsync(x => x.customerInvoiceId.Equals(customerInvoiceLine.customerInvoiceId));
                ci.subTotal = ci.customerInvoiceLine.Sum(x => x.amount);
                ci.taxAmount = ci.customerInvoiceLine.Sum(x => x.taxAmount);
                ci.grandTotal = ci.subTotal + ci.taxAmount - ci.discount + ci.shipping;
                _context.CustomerInvoice.Update(ci);

                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Edit data success." });
            }

        }

        // DELETE: api/CustomerInvoiceLine/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult>  DeleteCustomerInvoiceLine([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customerInvoiceLine = await _context.CustomerInvoiceLine.SingleOrDefaultAsync(m => m.customerInvoiceLineId == id);
            if (customerInvoiceLine == null)
            {
                return NotFound();
            }

            _context.CustomerInvoiceLine.Remove(customerInvoiceLine);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Delete success." });
        }


        private bool CustomerInvoiceLineExists(string id)
        {
            return _context.CustomerInvoiceLine.Any(e => e.customerInvoiceLineId == id);
        }


    }

}
