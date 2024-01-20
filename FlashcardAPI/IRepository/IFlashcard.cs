using FlashcardAPI.Data;
using FlashcardAPI.Models;

namespace FlashcardAPI.IRepository
{
    public interface IFlashcard
    {
        List<User> GetAllUsers();
        Task<Login> GetLogin(int userID);
        Task<LoginResponseModel> LoginUser(LoginModel data);
        Task<CardsResponseModel> GetCardInSet(int setId);
        Task<SetInfoResponseModel> GetSetInfo(int setId);
        Task<SetsResponseModel> GetAllSets();
        Task<SetInfoResponseModel> EditSetInfo(Set newSetInfo);
        Task<CardResponseModel> EditCardInfo(Card newCardInfo);
        Task<CardResponseModel> AddCard(Card inputCard);
        Task<SetInfoResponseModel> AddSet(Set inputSet);
        Task<SetsResponseModel> GetSetsInUserAccount(int userId);
        Task<FoldersResponseModel> GetFoldersInUserAccount(int userId);
        Task<SetInfoModel> GetSetsOnUserAccount(int userId);
        Task<FolderInfoModel> GetFoldersOnUserAccount(int userId);

        Task<ResponseModel> AddSetToFolder(int setID, int folderID);
        Task<SetInfoModel> GetFolderSets(int folderId);

        Task<CardResponseModel> DeleteCard(int cardId);
        Task<SetCreationResponseModel> CreateSetWithCard(SetCreationModel inputData);
        Task<FoldersResponseModel> deleteFolderFromSet (int setID, int folderID);
        Task<SetsResponseModel> DeleteSet(int setId);

        Task<ResponseModel> DeleteFolder(int folderId);

        Task<ResponseModel> AddFolder(Folder inputFolder);
        Task<SignUpModel> SignUp(SignUpData data);
    }
}
