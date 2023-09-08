using BondGadgetCollection.Data;
using BondGadgetCollection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BondGadgetCollection.Controllers
{
    public class GadgetsController : Controller
    {
        // GET: Gadgets
        [HttpGet]
        public ActionResult Index()
        {   
            GadgetDAO gadgetDAO = new GadgetDAO();
            List<GadgetModel> gadgets = gadgetDAO.FetchAll();
            return View("Index", gadgets);
        }

        public ActionResult Details(int id)
        {
            GadgetDAO gadgetDAO = new GadgetDAO();
            GadgetModel gadget = gadgetDAO.FetchOne(id);
            return View("Details", gadget);
        }
        
        public ActionResult Create()
        {
            return View("GadgetForm", new GadgetModel());
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            GadgetDAO gadgetDAO = new GadgetDAO();
            GadgetModel gadget = gadgetDAO.FetchOne(id);
            return View("GadgetForm", gadget);
        }

        [HttpPost]
        public ActionResult ProcessCreate(GadgetModel gadgetModel)
        {
            // Save to the database
            GadgetDAO gadgetDAO = new GadgetDAO();
            gadgetDAO.CreateOrUpdate(gadgetModel);

            return View("Details", gadgetModel);
        }

        public ActionResult Delete(int id)
        {
            GadgetDAO gadgetDAO = new GadgetDAO();
            gadgetDAO.Delete(id);
            List<GadgetModel> gadgets = gadgetDAO.FetchAll();
            return View("Index", gadgets);
        }

        public ActionResult SearchForm()
        {
            return View("SearchForm");
        }

        public ActionResult SearchForName(string searchPhrase)
        {
            // get a list of search results from the database

            GadgetDAO gadgetDAO =new GadgetDAO();
            List<GadgetModel> searchResult = gadgetDAO.SearchForName(searchPhrase);
            return View("Index", searchResult);
        }
    }
}