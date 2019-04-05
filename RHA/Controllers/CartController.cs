using RHA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RHA.Controllers
{
    public class CartController : Controller
    {
        private RHASTOREEntities db = new RHASTOREEntities();
        // GET: Cart
        public ActionResult Index()
        {
            var itemsincart = db.Carts.Include("Product").ToList();
           
            return View(itemsincart);
        }
        public ActionResult AddItemsToCart(int productid)
        {
            using (RHASTOREEntities db = new RHASTOREEntities())
            {
                if (User.Identity.Name == "")
                {
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    var currentuser_id = Convert.ToInt32(User.Identity.Name);//this is actually the unique user id,the name is misleading
                    var product_id = productid;
                    var quantity = 1;
                    Cart c = new Cart();

                    c.userid = currentuser_id;
                    c.ProductId = product_id;
                    c.Quantity = quantity;
                    db.Carts.Add(c);
                    db.SaveChanges();
                }


                return RedirectToAction("Index");
            }
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart product = db.Carts.Include("Product").Where(x=>x.Id==id).FirstOrDefault();
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Cart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cart product = db.Carts.Find(id);
            db.Carts.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}