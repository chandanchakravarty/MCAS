using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MCAS.Web.Objects.MastersHelper;
using MCAS.Web.Objects.CommonHelper;
using MCAS.Entity;

namespace MCAS.Controllers
{
    public class DiaryController : BaseController
    {
        //
        // GET: /Diary/
        MCASEntities _db = new MCASEntities();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DiaryIndex() {
            List<DiaryModel> list = new List<DiaryModel>();
            list = DiaryModel.FetchDiary();
            return View(list);
                        
        }

        [HttpPost]
        public ActionResult DiaryIndex(string SearchCriteria)
        {
            SearchCriteria = Request.Form["txtSearchCriteria"];
            List<DiaryModel> list = new List<DiaryModel>();
            list = GetSearchResult(SearchCriteria).ToList();
            return View(list);
        }

        public IQueryable<DiaryModel> GetSearchResult(string SearchCriteria)
        {
            
            var searchResult = (from survey in _db.TODODIARYLISTs
                                where
                                  survey.NOTE.Contains(SearchCriteria) 
                                  
                                select survey).ToList().Select(item => new DiaryModel
                                {
                                    Note = item.NOTE,
                                    SubjectLine = item.SUBJECTLINE,
                                    RecDate=Convert.ToDateTime(item.RECDATE),
                                    Follow_Up_dateTime=Convert.ToDateTime(item.FOLLOWUPDATE),
                                    ListTypeID=item.LISTTYPEID

                                 }).AsQueryable();

            return searchResult;
        }

        

        [HttpGet]
        public ActionResult DiaryEditor(int? ListId)
        {
            var diarylist = new DiaryModel();
            try {
                
                if (ListId.HasValue)
                {
                    var diary = (from lt in _db.TODODIARYLISTs where lt.LISTID == ListId select lt).FirstOrDefault();
                    DiaryModel model = new DiaryModel();
                    model.SubjectLine = diary.SUBJECTLINE;
                    model.Note = diary.NOTE;
                    model.ListTypeID = diary.LISTTYPEID;
                    model.ToUserId =Convert.ToInt32(diary.TOUSERID);
                    model.TypeList = LoadDiaryListType();
                    model.UserList = LoadUserList();
                    model.Follow_Up_dateTime = Convert.ToDateTime(diary.FOLLOWUPDATE);
                    model.priority = diary.PRIORITY;
                    model.Prioritylist = LoadLookUpValue("Priority");
                    model.SystemFollowupPid = Convert.ToInt32(diary.SYSTEMFOLLOWUPID);
                    model.Notificationlist = LoadLookUpValue("Notification");
                    return View(model);
                }
                diarylist.TypeList = LoadDiaryListType();
                diarylist.UserList = LoadUserList();
                diarylist.Notificationlist = LoadLookUpValue("Notification");
                diarylist.Prioritylist = LoadLookUpValue("Priority");
              
                    
            
            
            }
            catch (System.Data.DataException) { ModelState.AddModelError("", "Unable to View ."); }
            return View(diarylist);  

        }

        

        [HttpPost]
        public ActionResult DiaryEditor(DiaryModel model) {
            try
            {
                
                model.TypeList = LoadDiaryListType();
                model.UserList = LoadUserList();
                model.Notificationlist= LoadLookUpValue("Notification");
                model.Prioritylist = LoadLookUpValue("Priority");
                
                //TempData["notice"] = "Records Saved /Updated Successfully";
                if (ModelState.IsValid)
                {
                    var survey = model.DiaryUpdate();
                    TempData["notice"] = "Records Saved /Updated Successfully";

                    //  return RedirectToAction("LossNatureMasterList");
                }
                else
                {
                    TempData["notice"] = "Record already Exist.";
                    return View(model);
                }
            }
            catch (System.Data.DataException)
            {
                ModelState.AddModelError("", "Unable to save changes.");
            }
            return View(model);
        }


        [HttpPost]
        public ActionResult Delete(int? ListId)
        {

           MCASEntities _db=new MCASEntities();
            try
            {
                // TODO: Add delete logic here
                var csd = (from m in _db.TODODIARYLISTs where m.LISTID == ListId select m).FirstOrDefault();
                _db.TODODIARYLISTs.DeleteObject(csd);
                _db.SaveChanges();
               //return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
            return View("DiaryIndex");
            //List<DiaryViewModel> list = new List<DiaryViewModel>();
            //list = DiaryViewModel.FetchDiary();
            //list.RemoveAll(p => DiaryIdsToDelete.Contains(p.ListId));
            //return View("DiaryIndex");

            //// TODO: Perform the delete from a repository
            //_products.RemoveAll(p => productIdsToDelete.Contains(p.Id));
            //return RedirectToAction("index");
        }

        public JsonResult FillSearchOption()
        {
            var returnData = LoadSubClassCode();
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }

        //public IEnumerable<int> getSelectedIds()
        //{
        //    // Return an Enumerable containing the Id's of the selected people:
        //    return (from p in this.People where p.Selected select p.Id).ToList();
        //}

        
    }
}
