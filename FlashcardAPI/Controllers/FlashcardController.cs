using Microsoft.AspNetCore.Mvc;
using FlashcardAPI.Data;
using System.Collections.Generic;
using FlashcardAPI.IRepository;
using FlashcardAPI.Repository;
using FlashcardAPI.Models;
using Azure;
using Microsoft.EntityFrameworkCore;

namespace FlashcardAPI.Controllers
{
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //=> optionsBuilder.UseSqlServer(Configuration["ConnectionStrings:FlashcardDB2"]);
    [ApiController]
    [Route("[controller]")]
    public class FlashcardController : Controller
    {
        private readonly IFlashcard repository;

        public IConfiguration Configuration { get; }
        //context for the database connection
        private readonly FlashcardContext context;
        public FlashcardController(IConfiguration configuration)
        {
            Configuration = configuration;
            context = new FlashcardContext(Configuration);
            repository = new FlashcardDAL(context, Configuration);
        }


        #region Get All Users Method
        /// <summary>
        /// Method that retrieves all users in the database
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet("GetAllUsers")]

        public async Task<GetUsersResponseModel> GetAllUsers()
        {
            GetUsersResponseModel res = new GetUsersResponseModel();

            try
            {
                var allUsers = repository.GetAllUsers();
                if (allUsers != null)
                {
                    res.Status = true;
                    res.StatusCode = 200;
                    res.Message = "success";
                    res.userList = allUsers;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetAllUsers --- " + ex.Message);
                res.Status = false;
                res.StatusCode = 500;
                res.Message = ex.Message;
                throw;
            }

            return res;
        }

        #endregion


        #region Get Login
        /// <summary>
        /// Get login info of user for testing
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet("GetLogin")]

        public async Task<Login> GetLogin(int userID)
        {
            Login res = new Login();

            try
            {
                var login = await repository.GetLogin(userID);
                if (login != null)
                {
                    res = login;
                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }

            return res;
        }

        #endregion

        #region Login
        /// <summary>
        /// Get login info of user for testing
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost("LoginUser", Name = "LoginUser")]
        public async Task<LoginResponseModel> Login(LoginModel userData)
        {
            LoginResponseModel res = new LoginResponseModel();

            try
            {
                var a = await repository.LoginUser(userData).ConfigureAwait(true);
                if (a.StatusCode == 200)
                {
                    //Login is successful
                    res.Status = true;
                    res.Message = "login Successful";
                    res.StatusCode = 200;

                    //send back the user data object we updated
                    res.UserData = a.UserData;
                    res.IsLoggedIn = a.IsLoggedIn;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                res.Status = false;
                res.StatusCode = 500;
                res.Message = e.Message;
            }

            return res;
        }

        #endregion

        #region GetSetInfo
        /// <summary>
        /// GetSetInfo
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet("GetSetInfo")]

        public async Task<SetInfoResponseModel> GetSetInfo(int setId)
        { 
            SetInfoResponseModel response = new SetInfoResponseModel();
            try
            {
                var result = await repository.GetSetInfo(setId).ConfigureAwait(true);
                if(result.StatusCode == 200)
                {
                    response.Status = true;
                    response.StatusCode = 200;
                    response.Message = "success";
                    response.setInfo = result.setInfo;
                }
            }
            catch (Exception e)
            {
                response.Status = false;
                response.StatusCode = 500;
                response.Message = e.Message;
            }
            return response;
        }

        #endregion

        #region Get card in set
        /// <summary>
        /// Get cards based on the setid
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet("GetCardInSet")]

        public async Task<CardsResponseModel> GetCardInSet(int setId)
        {
            CardsResponseModel res = new CardsResponseModel();
            try
            {
                var result = await repository.GetCardInSet(setId).ConfigureAwait(true);
                if(result != null)
                {
                    res.Status = true;
                    res.StatusCode= 200;
                    res.Message = "success";
                    res.SetName = result.SetName;
                    res.cardsList = result.cardsList;

                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                res.Status = false;
                res.StatusCode = 500;
                res.Message = e.Message;
            }
            return res;
        }

        #endregion

        #region GetAllSets
        /// <summary>
        /// Get cards based on the setid
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet("GetAllSets")]

        public async Task<SetsResponseModel> GetAllSets()
        {
            SetsResponseModel response= new SetsResponseModel();
            try
            {
                var sets = await repository.GetAllSets();
                if(sets != null)
                {
                    response.Status = true;
                    response.StatusCode = 200;
                    response.Message = "success";
                    response.setList = sets.setList;
                }
            }
            catch(Exception e) 
            {
                response.Status = false;
                response.StatusCode = 500;
                response.Message = e.Message;
            }
            return response;
        }

        #endregion


        #region Edit Set
        /// <summary>
        /// Edit Set
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost("EditSet")]

        public async Task<SetInfoResponseModel> EditSet(Set newSetInfo)
        {
            SetInfoResponseModel res= new SetInfoResponseModel();

            try
            {

                var updateSet = await repository.EditSetInfo(newSetInfo);

                if(updateSet != null)
                {
                    res.Status = true;
                    res.StatusCode = 200;
                    res.Message = "success";
                    res.setInfo = updateSet.setInfo;
                }

            }catch(Exception e)
            {
                res.Status = false;
                res.StatusCode = 500;
                res.Message = e.Message;
            }

            return res;
        }
        #endregion

        #region Edit Card
        /// <summary>
        /// Edit Set
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost("EditCard")]

        public async Task<CardResponseModel> EditCard(Card newCardInfo)
        {
            CardResponseModel res = new CardResponseModel();
            try
            {
                var updateCard = await repository.EditCardInfo(newCardInfo);
                if(updateCard != null)
                {
                    res.Card = updateCard.Card;
                    res.Status = true;
                    res.StatusCode = 200;
                    res.Message = "success";
                }
            }
            catch (Exception e)
            {
                res.Status = false;
                res.StatusCode = 500;
                res.Message = e.Message;
            }
            return res;
        }

        #endregion

        #region Add Card
        /// <summary>
        /// Add Card
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost("AddCard")]

        public async Task<CardResponseModel> AddCard(Card newCardInfo)
        {
            CardResponseModel res = new CardResponseModel();
            try
            {
                var addNewCard = await repository.AddCard(newCardInfo);
                if (addNewCard.StatusCode == 200)
                {
                    res.Card = addNewCard.Card;
                    res.Status = true;
                    res.StatusCode = 200;
                    res.Message = "success";
                }
            }
            catch (Exception e)
            {
                res.Status = false;
                res.StatusCode = 500;
                res.Message = e.Message;
            }
            return res;
        }

        #endregion
        #region Add Set
        /// <summary>
        /// Add Set
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost("AddSet")]

        public async Task<SetInfoResponseModel> AddSet(Set newSetInfo)
        {
            SetInfoResponseModel res = new SetInfoResponseModel();
            try
            {
                var addNewSet = await repository.AddSet(newSetInfo);
                if (addNewSet.StatusCode == 200)
                {
                    res.setInfo = addNewSet.setInfo;
                    res.Status = true;
                    res.StatusCode = 200;
                    res.Message = "success";
                }
                else
                {
                    res.Status = false;
                    res.StatusCode = 0;
                    res.Message = addNewSet.Message;
                }
            }
            catch (Exception e)
            {
                res.Status = false;
                res.StatusCode = 500;
                res.Message = e.Message;
            }
            return res;
        }

        #endregion 

        #region GetSetsInUserAccount
        /// <summary>
        /// Get cards based on the setid
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet("GetSetsInUserAccount")]

        public async Task<SetsResponseModel> GetSetsInUserAccount(int userId)
        {
            SetsResponseModel response = new SetsResponseModel();
            try
            {
                var sets = await repository.GetSetsInUserAccount(userId);
                if (sets != null)
                {
                    response.Status = true;
                    response.StatusCode = 200;
                    response.Message = "success";
                    response.setList = sets.setList;
                }
            }
            catch (Exception e)
            {
                response.Status = false;
                response.StatusCode = 500;
                response.Message = e.Message;
            }
            return response;
        }

        #endregion
        #region GetSetsOnUserAccount
        /// <summary>
        /// Get sets base on user id
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet("GetSetsOnUserAccount")]

        public async Task<SetInfoModel> GetSetsOnUserAccount(int userId)
        {
            SetInfoModel response = new SetInfoModel();
            try
            {
                var sets = await repository.GetSetsOnUserAccount(userId);
                if (sets != null)
                {
                    response.Status = true;
                    response.StatusCode = 200;
                    response.Message = "success";
                    response.Data = sets.Data;
                }
            }
            catch (Exception e)
            {
                response.Status = false;
                response.StatusCode = 500;
                response.Message = e.Message;
            }
            return response;
        }

        #endregion


        #region GetFoldersOnUserAccount
        /// <summary>
        /// Get folders data base on user id
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet("GetFoldersOnUserAccount")]

        public async Task<FolderInfoModel> GetFoldersOnUserAccount(int userId)
        {
            FolderInfoModel response = new FolderInfoModel();
            try
            {
                var folders = await repository.GetFoldersOnUserAccount(userId);
                if (folders != null)
                {
                    response.Status = true;
                    response.StatusCode = 200;
                    response.Message = "success";
                    response.Data = folders.Data;
                }
            }
            catch (Exception e)
            {
                response.Status = false;
                response.StatusCode = 500;
                response.Message = e.Message;
            }
            return response;
        }

        #endregion

        #region AddSetToFolder
        /// <summary>
        /// Change Set folder
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost("AddSetToFolder")]

        public async Task<ResponseModel> AddSetToFolder(int setID, int folderID)
        {
            ResponseModel res = new ResponseModel();

            try
            {

                var updateSet = await repository.AddSetToFolder( setID,  folderID);

                if (updateSet != null)
                {
                    res.Status = updateSet.Status;
                    res.StatusCode = updateSet.StatusCode;
                    res.Message = updateSet.Message;

                }

            }
            catch (Exception e)
            {
                res.Status = false;
                res.StatusCode = 500;
                res.Message = e.Message;
            }

            return res;
        }
        #endregion
        #region GetFolderSets
        /// <summary>
        /// Get sets base on folder id
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet("GetFolderSets")]

        public async Task<SetInfoModel> GetFolderSets(int folderId)
        {
            SetInfoModel response = new SetInfoModel();
            try
            {
                var sets = await repository.GetFolderSets(folderId);
                if (sets != null)
                {
                    response.Status = true;
                    response.StatusCode = 200;
                    response.Message = "success";
                    response.Data = sets.Data;
                    response.FolderTitle = sets.FolderTitle;    
                }
            }
            catch (Exception e)
            {
                response.Status = false;
                response.StatusCode = 500;
                response.Message = e.Message;
            }
            return response;
        }

        #endregion
        #region Delete Card
        /// <summary>
        /// Delete Card
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost("DeleteCard")]

        public async Task<CardResponseModel> DeleteCard(int cardId)
        {
            CardResponseModel res = new CardResponseModel();
            try
            {
                var deleteCard = await repository.DeleteCard(cardId);
                if (deleteCard.StatusCode == 200)
                {
                    res.Status = true;
                    res.StatusCode = 200;
                    res.Message = "success";
                }
            }
            catch (Exception e)
            {
                res.Status = false;
                res.StatusCode = 500;
                res.Message = e.Message;
            }
            return res;
        }

        #endregion

        #region CreateSetWithCard
        /// <summary>
        /// Create Sets with cards
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost("CreateSetWithCard")]

        public async Task<SetCreationResponseModel> CreateSetWithCard(SetCreationModel inputData)
        {
            SetCreationResponseModel res = new SetCreationResponseModel();
            try
            {
                var addNewSet = await repository.CreateSetWithCard(inputData);
                if (addNewSet.StatusCode == 200)
                {
                    res.SetDescription = inputData.SetDescription;
                    res.SetTitle = inputData.SetTitle;
                    res.CardList = inputData.CardList;
                    res.Status = true;
                    res.StatusCode = 200;
                    res.Message = "success";
                }
                else
                {
                    res.Status = false;
                    res.StatusCode = 0;
                    res.Message = addNewSet.Message;
                }
            }
            catch (Exception e)
            {
                res.Status = false;
                res.StatusCode = 500;
                res.Message = e.Message;
            }
            return res;
        }

        #endregion

        #region Delete folder from set
        /// <summary>
        /// Change the set folder to none
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost("DeleteFolderFromSet")]

        public async Task<FoldersResponseModel> deleteFolderFromSet(int setID, int folderID)
        {
            FoldersResponseModel res = new FoldersResponseModel();
            try
            {
                var setData = await repository.deleteFolderFromSet(setID,folderID);
                if (setData.StatusCode == 200)
                {
                    res = setData;

                    res.Status = true;
                    res.StatusCode = 200;
                    res.Message = "success";
                }
                else
                {
                    res.Status = false;
                    res.StatusCode = 0;
                    res.Message = setData.Message;
                }
            }
            catch (Exception e)
            {
                res.Status = false;
                res.StatusCode = 500;
                res.Message = e.Message;
            }
            return res;
        }

        #endregion 

        #region Delete Set
        /// <summary>
        /// Delete Set
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost("DeleteSet")]

        public async Task<SetsResponseModel> DeleteSet(int setId)
        {
            SetsResponseModel res = new SetsResponseModel();
            try
            {
                var deleteCard = await repository.DeleteSet(setId);
                if (deleteCard.StatusCode == 200)
                {   
                    res.Status = true;
                    res.StatusCode = 200;
                    res.Message = "success";
                }
            }
            catch (Exception e)
            {
                res.Status = false;
                res.StatusCode = 500;
                res.Message = e.Message;
            }
            return res;
        }

        #endregion

        #region Delete Folder
        /// <summary>
        /// Delete Folder
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost("DeleteFolder")]

        public async Task<ResponseModel> DeleteFolder(int folderId)
        {
            ResponseModel res = new ResponseModel();
            try
            {
                var deleteCard = await repository.DeleteFolder(folderId);
                if (deleteCard.StatusCode == 200)
                {
                    res.Status = true;
                    res.StatusCode = 200;
                    res.Message = "success";
                }
            }
            catch (Exception e)
            {
                res.Status = false;
                res.StatusCode = 500;
                res.Message = e.Message;
            }
            return res;
        }

        #endregion

        #region Add Folder
        /// <summary>
        /// Add Folder
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost("AddFolder")]

        public async Task<ResponseModel> AddFolder(Folder inputFolder)
        {
            ResponseModel res = new ResponseModel();
            try
            {
                var deleteCard = await repository.AddFolder(inputFolder);
                if (deleteCard.StatusCode == 200)
                {
                    res.Status = true;
                    res.StatusCode = 200;
                    res.Message = "success";
                }
            }
            catch (Exception e)
            {
                res.Status = false;
                res.StatusCode = 500;
                res.Message = e.Message;
            }
            return res;
        }

        #endregion

        #region Add User And Login
        /// <summary>
        /// Add User And Login
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost("SignUp")]

        public async Task<SignUpModel> SignUp(SignUpData data)
        {
            SignUpModel res = new SignUpModel();
            try
            {
                var signUp = await repository.SignUp(data);
                res.Message = signUp.Message;
                res.UserId = signUp.UserId;
                if (signUp.StatusCode == 200)
                {
                    res.Status = true;
                    res.StatusCode = 200;
                    
                }
            }
            catch (Exception e)
            {
                res.Status = false;
                res.StatusCode = 500;
                res.Message = e.Message;
            }
            return res;
        }

        #endregion


    }
}
