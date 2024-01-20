using FlashcardAPI.Data;
using FlashcardAPI.IRepository;
using FlashcardAPI.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System.Collections.Generic;

namespace FlashcardAPI.Repository
{
    public class FlashcardDAL : IFlashcard
    {
        private readonly FlashcardContext context;
        private readonly IConfiguration _config;

        public FlashcardDAL(FlashcardContext Context, IConfiguration config)
        {
            context = Context;
            _config = config;

        }



        #region Get All Users Method
        /// <summary>
        /// Method that retrieves all users in the database
        /// </summary>
        /// <returns></returns>
        public List<User> GetAllUsers()
        {
            List<User> userList = new List<User>();

            try
            {
                //query the database to get all of the users
                var users = context.Users.ToList();

                foreach (var user in users)
                {
                    userList.Add(user);
                }
                if (userList.Count == 0)
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetAllUsers --- " + ex.Message);
                throw;
            }

            return userList;
        }

        #endregion


        #region Get login details for testing

        public async Task<Login> GetLogin(int userID)
        {
            Login res = new Login();

            try
            {
                var users = context.Logins.Where(x => x.UserId == userID).FirstOrDefault();
                if (users != null)
                {
                    res = users;
                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }

            return res;
        }
        #endregion

        #region Login User Method
        public async Task<LoginResponseModel> LoginUser(LoginModel data)
        {
            LoginResponseModel res = new LoginResponseModel();
            try
            {
                if (data != null)
                {
                    //look for the user in the database
                    var query = context.Logins
                    .Where(x => x.Username == data.Username && x.Password == data.Password)
                    .FirstOrDefault<Login>();

                    //if query has a result then we have a match
                    if (query != null)
                    {
                        res.Status = true;
                        res.StatusCode = 200;
                        res.Message = "Login Success";
                        res.IsLoggedIn = true;
                        //get the user data so we can send it back with
                        var user = context.Users.Where(x => x.UserId == query.UserId).FirstOrDefault();
                        if (user != null)
                        {
                            res.UserData = new User
                            {
                                UserId = user.UserId,
                                FirstName = user.FirstName,
                                LastName = user.LastName,
                                Email = user.Email,
                            };
                            return res;
                        }

                    }
                    else
                    {
                        //the user wasn't found or wasn't a match
                        res.Status = false;
                        res.StatusCode = 500;
                        res.Message = "Login Failed";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("LoginUser --- " + ex.Message);
                res.Status = false;
                res.StatusCode = 500;
            }

            return res;
        }
        #endregion

        #region GetAllSets
        public async Task<SetsResponseModel> GetAllSets()
        {
            SetsResponseModel res = new SetsResponseModel();
            try
            {
                var setList = context.Sets.ToList();
                if (setList != null)
                {
                    res.setList = setList;
                }

            } catch (Exception ex)
            {
                Console.WriteLine("Get all sets in set --- " + ex.Message);
                res.Status = false;
                res.StatusCode = 500;
                res.Message = ex.Message;
            }
            return res;
        }

        #endregion

        #region GetSetInfo
        public async Task<SetInfoResponseModel> GetSetInfo(int setId)
        {
            SetInfoResponseModel res = new SetInfoResponseModel();
            try
            {
                if (setId != 0)
                {
                    var set = context.Sets.Where(x => x.SetId == setId).FirstOrDefault();
                    if (set != null)
                    {
                        res.setInfo = set;
                        res.Status = true;
                        res.StatusCode = 200;
                        res.Message = "success";
                    }
                }
                else
                {
                    res.Message = "id not found";
                    res.Status = false;
                    res.StatusCode = 500;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Get card in set --- " + ex.Message);
                res.Status = false;
                res.StatusCode = 500;
            }
            return res;
        }

        #endregion


        #region Get cards in a set
        public async Task<CardsResponseModel> GetCardInSet(int setId)
        {
            CardsResponseModel res = new CardsResponseModel();
            try
            {
                if (setId != null)
                {
                    var setInfo = context.Sets.Where(x => x.SetId == setId).FirstOrDefault();
                    var cards = context.Cards.Where(x => x.SetId == setId).ToList();
                    if (cards.Count != 0 && setInfo != null)
                    {
                        res.SetName = setInfo.SetTitle;
                        res.cardsList = cards;
                        res.Status = true;
                        res.StatusCode = 200;
                        res.Message = "success";

                    }
                }
                else
                {
                    Console.WriteLine("Get card in set - set is null ");
                    res.Status = false;
                    res.StatusCode = 500;
                    res.Message = "failed";

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Get card in set --- " + ex.Message);
                res.Status = false;
                res.StatusCode = 500;
                res.Message = ex.Message;
            }

            return res;
        }


        #endregion

        #region Edit Sets 1
        public async Task<SetInfoResponseModel> EditSetInfo(Set newSetInfo)
        {
            SetInfoResponseModel res = new SetInfoResponseModel();
            try
            {
                //open database connection
                using (var db = new FlashcardContext(_config))
                {

                }

            }
            catch (Exception ex)
            {
                res.Status = false;
                res.StatusCode = 500;
                res.Message = ex.Message;
            }
            return res;
        }
        #endregion

        #region Edit Card
        public async Task<CardResponseModel> EditCardInfo(Card newCardInfo)
        {
            CardResponseModel res = new CardResponseModel();
            try
            {
                //open database connection
                using (var db = new FlashcardContext(_config))
                {
                    var cardInfo = db.Cards.Where(x => x.CardId == newCardInfo.CardId).FirstOrDefault();
                    if (cardInfo != null)
                    {
                        cardInfo.CardFront = newCardInfo.CardFront;
                        cardInfo.CardBack = newCardInfo.CardBack;
                        cardInfo.Starred = newCardInfo.Starred;

                        db.SaveChanges();

                        res.Card = cardInfo;
                        res.Status = true;
                        res.StatusCode = 200;
                        res.Message = "success";
                    }
                }
                }
            catch (Exception ex)
            {
                res.Status = false;
                res.StatusCode = 500;
                res.Message = ex.Message;
            }
            return res;
        }
        #endregion



        #region Add Card
        public async Task<CardResponseModel> AddCard(Card inputCard)
        {
            CardResponseModel res = new CardResponseModel();
            try
            {
                

                if (inputCard != null) {
                    //get and set highest id + 1 for new card
/*                    var highestID = context.Cards.Max(x => x.CardId);
                    inputCard.CardId = highestID+1;*/

                    //save this user in the database
                    using (var db = new FlashcardContext(_config))
                    {
                        db.Cards.Add(inputCard);
                        db.SaveChanges();

                        res.Card = inputCard;
                        res.Status = true;
                        res.StatusCode = 200;
                        res.Message = "success";
                    } 
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Add card dal --- " + ex.Message);
                res.Status = false;
                res.StatusCode = 500;
                res.Message = ex.Message;
            }
            return res;
        }

        #endregion

        #region Add Set
        public async Task<SetInfoResponseModel> AddSet(Set inputSet)
        {
            SetInfoResponseModel res = new SetInfoResponseModel();
            try
            {


                if (inputSet != null)
                {
                    //get and set highest id + 1 for new card
/*                    var highestID = context.Sets.Max(x => x.SetId);
                    inputSet.SetId = highestID + 1;*/

                    //save this user in the database
                    using (var db = new FlashcardContext(_config))
                    {
                        db.Sets.Add(inputSet);
                        db.SaveChanges();

                        res.setInfo = inputSet;
                        res.Status = true;
                        res.StatusCode = 200;
                        res.Message = "success";
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("add set dal--- " + ex.Message);
                res.Status = false;
                res.StatusCode = 500;
                res.Message = ex.Message;
            }
            return res;
        }

        #endregion

        #region Get Sets in user account
        public async Task<SetsResponseModel> GetSetsInUserAccount(int userId)
        {
            SetsResponseModel res = new SetsResponseModel();
            try
            {
                if (userId != null)
                {
                    var setList = context.Sets.Where(x => x.UserId == userId).ToList();

                    if (setList.Count != 0)
                    {
                        res.setList = setList;
                        res.Status = true;
                        res.StatusCode = 200;
                        res.Message = "success";

                    }
                }
                else
                {
                    Console.WriteLine("Get set in user acc - set is null ");
                    res.Status = false;
                    res.StatusCode = 500;
                    res.Message = "failed";

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Get set in user acc --- " + ex.Message);
                res.Status = false;
                res.StatusCode = 500;
                res.Message = ex.Message;
            }

            return res;
        }


        #endregion

        #region Get Folders in user account
        public async Task<FoldersResponseModel> GetFoldersInUserAccount(int userId)
        {
            FoldersResponseModel res = new FoldersResponseModel();
            try
            {
                if (userId != null)
                {
                    var folderList = context.Folders.Where(x => x.UserId == userId).ToList();

                    if (folderList.Count != 0)
                    {
                        res.Folders = folderList;
                        res.Status = true;
                        res.StatusCode = 200;
                        res.Message = "success";

                    }
                }
                else
                {
                    Console.WriteLine("Get folder in user acc - set is null ");
                    res.Status = false;
                    res.StatusCode = 500;
                    res.Message = "failed";

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Get folder in user acc --- " + ex.Message);
                res.Status = false;
                res.StatusCode = 500;
                res.Message = ex.Message;
            }

            return res;
        }


        #endregion

        #region Get Sets on user account
        public async Task<SetInfoModel> GetSetsOnUserAccount(int userId)
        {
            SetInfoModel res = new SetInfoModel();
            try
            {
                if (userId != null)
                {
                    var setList = context.Sets.Where(x => x.UserId == userId).ToList();
                    var findUser = context.Logins.Where(x=>x.UserId == userId).FirstOrDefault();
                    
                    if (setList.Count != 0)
                    {
                        res.Data = new List<SetData>();
                        foreach ( var set in setList)
                        {
                            SetData setData = new SetData();
                            setData.SetId = set.SetId;
                            setData.UserId = userId;
                            setData.SetTitle = set.SetTitle;
                            setData.SetDescription = set.SetDescription;
                            setData.UserName = findUser.Username; 
                            setData.CardCount = context.Cards.Where(x=> x.SetId == set.SetId).Count();

                            var setFolders = context.SetFolders.Where(x => x.SetId == set.SetId).ToList();
                            if (setFolders.Count != 0)
                            {
                                List<int> connectionList = new List<int>();
                                foreach ( var folderConnection in setFolders)
                                {
                                    connectionList.Add(folderConnection.FolderId);
                                }
                                setData.FolderConnection = connectionList;
                            }
                            else
                            {
                                setData.FolderConnection = new List<int> { 0 };
                            }

                            res.Data.Add(setData);
                        }
                        res.Status = true;
                        res.StatusCode = 200;
                        res.Message = "success";

                    }
                }
                else
                {
                    Console.WriteLine("Get set in user acc - set is null ");
                    res.Status = false;
                    res.StatusCode = 500;
                    res.Message = "failed";

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Get set in user acc --- " + ex.Message);
                res.Status = false;
                res.StatusCode = 500;
                res.Message = ex.Message;
            }

            return res;
        }


        #endregion
        #region Get Folders on user account 
        public async Task<FolderInfoModel> GetFoldersOnUserAccount(int userId)
        {
            FolderInfoModel res = new FolderInfoModel();
            try
            {
                if (userId != null)
                {
                    var folderList = context.Folders.Where(x => x.UserId == userId).ToList();
                    var findUser = context.Logins.Where(x => x.UserId == userId).FirstOrDefault();

                    if (folderList.Count != 0)
                    {
                        res.Data = new List<FolderData>();
                        foreach (var folder in folderList)
                        {
                            FolderData folderData = new FolderData();
                            folderData.FolderId = folder.FolderId;
                            folderData.UserId = userId;
                            folderData.FolderTitle = folder.FolderTitle;
                            folderData.FolderDescription = folder.FolderDescription;
                            folderData.UserName = findUser.Username;
                            folderData.SetCount = context.SetFolders.Where(x => x.FolderId == folder.FolderId).Count();
                            res.Data.Add(folderData);
                        }
                        res.Status = true;
                        res.StatusCode = 200;
                        res.Message = "success";

                    }
                }
                else
                {
                    Console.WriteLine("Get folders in user acc - set is null ");
                    res.Status = false;
                    res.StatusCode = 500;
                    res.Message = "failed";

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Get folders in user acc --- " + ex.Message);
                res.Status = false;
                res.StatusCode = 500;
                res.Message = ex.Message;
            }

            return res;
        }


        #endregion

        #region AddSetToFolder
        public async Task<ResponseModel> AddSetToFolder(int setID, int folderID)
        {
            ResponseModel res = new ResponseModel();
            try
            {


                if (setID != null && folderID != null)
                {

                    //open database connection
                    using (var db = new FlashcardContext(_config))
                    {
                        var set = db.Sets.Where((x) => x.SetId == setID).FirstOrDefault();
                        var folder = db.Folders.Where((x) => x.FolderId == folderID).FirstOrDefault();

                        if (set != null && folder != null)
                        {
                            //check if there is a existing connection
                            var checkForExisting = db.SetFolders.Where(x => x.FolderId == folderID && x.SetId == setID).FirstOrDefault();
                            //if there isnt
                            if (checkForExisting == null) { 
                                SetFolder newSetFolder = new SetFolder();
                                newSetFolder.SetId = setID;
                                newSetFolder.FolderId = folderID;
                                db.SetFolders.Add(newSetFolder);
                                db.SaveChanges();
                                res.Status = true;
                                res.StatusCode = 200;
                                res.Message = "success";
                            }
                            else //if there is set status to fail
                            {
                                res.Status = false;
                                res.StatusCode = 400;
                                res.Message = "set folder connect exist already";
                            }
                        
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("add set to folder dal--- " + ex.Message);
                res.Status = false;
                res.StatusCode = 500;
                res.Message = ex.Message;
            }
            return res;
        }

        #endregion

        #region Get folder sets 1
        public async Task<SetInfoModel> GetFolderSets(int folderId)
        {
            SetInfoModel res = new SetInfoModel();
            try
            {
                if (folderId != null)
                {
                    var setConnectionList = context.SetFolders.Where(x => x.FolderId == folderId).ToList();
                    res.Data = new List<SetData>();

                    var folder = context.Folders.Where(x => x.FolderId == folderId).FirstOrDefault();
                    res.FolderTitle = folder.FolderTitle;
                    if (setConnectionList.Count != 0)
                    {

                        foreach (var setConnection in setConnectionList)
                        {

                            var set = context.Sets.Where(x => x.SetId == setConnection.SetId).FirstOrDefault();

                            //find user by looking at userid from the set
                            var user = context.Logins.Where(x => x.UserId == set.UserId).FirstOrDefault();

                            SetData setData = new SetData();
                            setData.SetId = set.SetId;
                            setData.UserId = user.UserId;
                            setData.SetTitle = set.SetTitle;
                            setData.SetDescription = set.SetDescription;
                            setData.UserName = user.Username;
                            setData.CardCount = context.Cards.Where(x => x.SetId == set.SetId).Count();
                            res.Data.Add(setData);
                        }
                        res.Status = true;
                        res.StatusCode = 200;
                        res.Message = "success";

                    }
                }
                else
                {
                    Console.WriteLine("Get set in user acc - set is null ");
                    res.Status = false;
                    res.StatusCode = 500;
                    res.Message = "failed";

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Get set in user acc --- " + ex.Message);
                res.Status = false;
                res.StatusCode = 500;
                res.Message = ex.Message;
            }

            return res;
        }


        #endregion
        #region Delete Card
        public async Task<CardResponseModel> DeleteCard(int cardId)
        {
            CardResponseModel res = new CardResponseModel();
            try
            {
                using (var db = new FlashcardContext(_config))
                {
                    var cardToDelete =  db.Cards.Where(x => x.CardId == cardId).FirstOrDefault();

                    if (cardToDelete != null)
                    {
                        db.Cards.Remove(cardToDelete);
                        await db.SaveChangesAsync();

                        res.Status = true;
                        res.StatusCode = 200;
                        res.Message = "Card deleted successfully";
                    }
                    else
                    {
                        res.Status = false;
                        res.StatusCode = 404; // Not Found
                        res.Message = "Card not found";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Delete card dal --- " + ex.Message);
                res.Status = false;
                res.StatusCode = 500;
                res.Message = ex.Message;
            }
            return res;
        }
        #endregion

        #region Create Set With Card
        public async Task<SetCreationResponseModel> CreateSetWithCard(SetCreationModel inputData)
        {
            SetCreationResponseModel res = new SetCreationResponseModel();
            try
           {
                if (inputData != null)
                {

                    //save this user in the database
                    using (var db = new FlashcardContext(_config))
                    {
                        //create new set and add to db
                        Set newSet = new Set();
                        newSet.UserId = inputData.UserID;
                        newSet.SetTitle = inputData.SetTitle;
                        newSet.SetDescription = inputData.SetDescription;

                        db.Sets.Add(newSet);
                        db.SaveChanges();

                        //get and set highest id for new card
                        var highestID = context.Sets.Max(x => x.SetId);
                        

                        if (inputData.CardList.Count() != 0)
                        {
                            foreach (var card in inputData.CardList)
                            {
                                Card newCard = new Card();
                                newCard.SetId = highestID;
                                newCard.CardFront = card.CardFront;
                                newCard.CardBack = card.CardBack;
                                newCard.Starred = false;
                                db.Cards.Add(newCard);
                                
                            }
                            res.Status = false;
                            res.StatusCode = 200;
                            res.Message = "success";
                            db.SaveChanges();
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("create set with folder dal--- " + ex.Message);
                res.Status = false;
                res.StatusCode = 500;
                res.Message = ex.Message;
            }
            return res;
        }

        #endregion

        #region Delete folder from set1
        public async Task<FoldersResponseModel> deleteFolderFromSet(int setId,int folderID)
        {
            FoldersResponseModel res = new FoldersResponseModel();
            try
            {
                using (var db = new FlashcardContext(_config))
                {
                    var setConnection = db.SetFolders.Where(x => x.SetId == setId && x.FolderId == folderID).FirstOrDefault();

                    if (setConnection != null)
                    {
                        db.SetFolders.Remove(setConnection);
                        db.SaveChanges();

                        res.Status = true;
                        res.StatusCode = 200;
                        res.Message = "Set folder connection deleted successfully";
                    }
                    else
                    {
                        res.Status = false;
                        res.StatusCode = 404; // Not Found
                        res.Message = "Set folder connection not found";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Delete set from folder dal --- " + ex.Message);
                res.Status = false;
                res.StatusCode = 500;
                res.Message = ex.Message;
            }
            return res;
        }
        #endregion


        #region Delete Set
        public async Task<SetsResponseModel> DeleteSet(int setId)
        {
            SetsResponseModel res = new SetsResponseModel();
            try
            {
                using (var db = new FlashcardContext(_config))
                {
                    var setToDelete = db.Sets.Where(x => x.SetId == setId).FirstOrDefault();

                    if (setToDelete != null)
                    {
                        db.Sets.Remove(setToDelete);
                        await db.SaveChangesAsync();

                        res.Status = true;
                        res.StatusCode = 200;
                        res.Message = "Set deleted successfully";
                    }
                    else
                    {
                        res.Status = false;
                        res.StatusCode = 404; // Not Found
                        res.Message = "Set not found";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Delete card dal --- " + ex.Message);
                res.Status = false;
                res.StatusCode = 500;
                res.Message = ex.Message;
            }
            return res;
        }
        #endregion

        #region Delete Folder
        public async Task<ResponseModel> DeleteFolder(int folderId)
        {
            ResponseModel res = new ResponseModel();
            try
            {
                using (var db = new FlashcardContext(_config))
                {
                    var folderToDelete = db.Folders.Where(x => x.FolderId == folderId).FirstOrDefault();

                    if (folderToDelete != null)
                    {
                        db.Folders.Remove(folderToDelete);
                        await db.SaveChangesAsync();

                        res.Status = true;
                        res.StatusCode = 200;
                        res.Message = "Folder deleted successfully";
                    }
                    else
                    {
                        res.Status = false;
                        res.StatusCode = 404; // Not Found
                        res.Message = "Folder not found";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Folder card dal --- " + ex.Message);
                res.Status = false;
                res.StatusCode = 500;
                res.Message = ex.Message;
            }
            return res;
        }
        #endregion

        #region Add Folder
        public async Task<ResponseModel> AddFolder(Folder inputFolder)
        {
            ResponseModel res = new ResponseModel();
            try
            {


                if (inputFolder.UserId != null && inputFolder.FolderTitle != null)
                {
                    //get and set highest id + 1 for new card
                    /*                    var highestID = context.Cards.Max(x => x.CardId);
                                        inputCard.CardId = highestID+1;*/

                    //save this user in the database
                    using (var db = new FlashcardContext(_config))
                    {
                        db.Folders.Add(inputFolder);
                        db.SaveChanges();

                        res.Status = true;
                        res.StatusCode = 200;
                        res.Message = "success";
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Add Folder dal --- " + ex.Message);
                res.Status = false;
                res.StatusCode = 500;
                res.Message = ex.Message;
            }
            return res;
        }

        #endregion
        #region signup
        public async Task<SignUpModel> SignUp(SignUpData data)
        {
            SignUpModel res = new SignUpModel();
            try
            {

                //check input is not null
                if (data.firstName != null && data.lastName != null && data.email != null && data.username != null && data.password != null)
                {
                    User user = new User();
                    user.FirstName = data.firstName;
                    user.LastName = data.lastName;
                    user.Email = data.email;

                    Login login = new Login();
                    login.Username = data.username;
                    login.Password = data.password;

                    using (var db = new FlashcardContext(_config))
                    {
                        db.Users.Add(user);
                        db.SaveChanges();

                        var recentUser = db.Users.Max(x=> x.UserId);
                        res.UserId = recentUser;
                        login.UserId = recentUser;
                        db.Logins.Add(login);
                        db.SaveChanges();

                    }
                    res.Status = true;
                    res.StatusCode = 200;
                    res.Message = "success";
                }
                else
                {
                    Console.WriteLine("signup dal --- ");
                    res.Status = false;
                    res.StatusCode = 500;
                    res.Message = "null inputs";
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("signup dal --- " + ex.Message);
                res.Status = false;
                res.StatusCode = 500;
                res.Message = ex.Message;
            }
            return res;
        }

        #endregion



    }
}
