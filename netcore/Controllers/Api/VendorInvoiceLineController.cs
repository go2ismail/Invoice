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
    [Route("api/VendorInvoiceLine")]
    public class VendorInvoiceLineController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VendorInvoiceLineController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/VendorInvoiceLine
        [HttpGet]
        [Authorize]
        public IActionResult GetVendorInvoiceLine(string masterid)
        {
            return Json(new { data = _context.VendorInvoiceLine.Include(x => x.item).Where(x => x.vendorInvoiceId.Equals(masterid)).ToList() });
        }

        // POST: api/VendorInvoiceLine
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostVendorInvoiceLine([FromBody] VendorInvoiceLine vendorInvoiceLine)
        {
            Item item = await _context.Item.Include(x => x.tax).SingleOrDefaultAsync(x => x.itemId.Equals(vendorInvoiceLine.itemId));
            vendorInvoiceLine.taxId = item.taxId;


            if (vendorInvoiceLine.vendorInvoiceLineId == string.Empty)
            {
                vendorInvoiceLine.vendorInvoiceLineId = Guid.NewGuid().ToString();

                if (vendorInvoiceLine.price.Equals(0m)) vendorInvoiceLine.price = item.price;
                vendorInvoiceLine.amount = (decimal)vendorInvoiceLine.quantity * vendorInvoiceLine.price;
                vendorInvoiceLine.taxAmount = (decimal)(item.tax.taxRate / 100.0) * vendorInvoiceLine.amount;
                vendorInvoiceLine.totalAmount = vendorInvoiceLine.amount + vendorInvoiceLine.taxAmount;
                _context.VendorInvoiceLine.Add(vendorInvoiceLine);

                VendorInvoice vi = await _context.VendorInvoice.Include(x => x.vendorInvoiceLine).SingleOrDefaultAsync(x => x.vendorInvoiceId.Equals(vendorInvoiceLine.vendorInvoiceId));
                vi.subTotal = vi.vendorInvoiceLine.Sum(x => x.amount);
                vi.taxAmount = vi.vendorInvoiceLine.Sum(x => x.taxAmount);
                vi.grandTotal = vi.subTotal + vi.taxAmount - vi.discount + vi.shipping;
                _context.VendorInvoice.Update(vi);

                _context.VendorInvoiceLine.Add(vendorInvoiceLine);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Add new data success." });
            }
            else
            {
                if (vendorInvoiceLine.price.Equals(0m)) vendorInvoiceLine.price = item.price;
                vendorInvoiceLine.amount = (decimal)vendorInvoiceLine.quantity * vendorInvoiceLine.price;
                vendorInvoiceLine.taxAmount = (decimal)(item.tax.taxRate / 100.0) * vendorInvoiceLine.amount;
                vendorInvoiceLine.totalAmount = vendorInvoiceLine.amount + vendorInvoiceLine.taxAmount;
                _context.Update(vendorInvoiceLine);

                VendorInvoice vi = await _context.VendorInvoice.Include(x => x.vendorInvoiceLine).SingleOrDefaultAsync(x => x.vendorInvoiceId.Equals(vendorInvoiceLine.vendorInvoiceId));
                vi.subTotal = vi.vendorInvoiceLine.Sum(x => x.amount);
                vi.taxAmount = vi.vendorInvoiceLine.Sum(x => x.taxAmount);
                vi.grandTotal = vi.subTotal + vi.taxAmount - vi.discount + vi.shipping;
                _context.VendorInvoice.Update(vi);

                _context.Update(vendorInvoiceLine);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Edit data success." });
            }

        }

        // DELETE: api/VendorInvoiceLine/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult>  DeleteVendorInvoiceLine([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vendorInvoiceLine = await _context.VendorInvoiceLine.SingleOrDefaultAsync(m => m.vendorInvoiceLineId == id);
            if (vendorInvoiceLine == null)
            {
                return NotFound();
            }

            _context.VendorInvoiceLine.Remove(vendorInvoiceLine);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Delete success." });
        }


        private bool VendorInvoiceLineExists(string id)
        {
            return _context.VendorInvoiceLine.Any(e => e.vendorInvoiceLineId == id);
        }


    }

}
