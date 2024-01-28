using MauiBlazor.Shared.Models;

namespace MauiBlazor.Services;

public interface ICardService
{
  Task<ResponseModel<List<CardModel>>> GetHomePageDetails();
  Task<ResponseModel<string>> AddCard(CardModel cardModel);
  Task<ResponseModel<string>> UpdateCard(CardModel cardModel); 
  Task<ResponseModel<string>> DeleteCard(CardModel cardModel);
  Task<ResponseModel<CardModel>> GetCardDetailByCardId(int cardId);
  Task<ResponseModel<List<CardModel>>> GetSavedCardsByUserId();
  Task<ResponseModel<string>> SaveCardToUser(int cardId);
}