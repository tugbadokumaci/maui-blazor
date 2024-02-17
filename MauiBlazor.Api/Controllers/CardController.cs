using System;
using MauiBlazor.Api.Data;
using MauiBlazor.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MauiBlazor.Api.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
public class CardController : ControllerBase
{
    private readonly MyDbContext _dbContext;

    public CardController(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("getAllCards")]
    public IActionResult GetAllCards()
    {
        try
        {
            var allCards = _dbContext.Cards.ToList();
            return Ok(allCards);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }

    [HttpGet("getUserCards/{Id}")]
    public IActionResult GetUserCards(String userId)
    {
        try
        {
            var userCards = _dbContext.Cards.Where(c => c.CreatorId == userId).ToList();
            return Ok(userCards);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }

    [HttpGet("getCardDetails/{Id}")]
    public IActionResult GetCardDetails(int Id)
    {
        try
        {
            var card = _dbContext.Cards.FirstOrDefault(c => c.Id == Id);

            if (card == null)
                return NotFound("Card not found.");

            return Ok(card);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }

    [HttpPost("createCardByUserId/{userId}")]
    public IActionResult CreateCard(String userId, [FromBody] CardModel newCard)
    {
        try
        {
            newCard.CreatorId = userId;
            _dbContext.Cards.Add(newCard);
            _dbContext.SaveChanges();
            return CreatedAtAction(nameof(GetCardDetails), new { userId, Id = newCard.Id }, newCard);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }

    [HttpPut("updateCard/{Id}")]
    public IActionResult UpdateCard(String userId, int Id, [FromBody] CardModel updatedCard)
    {
        
        try
        {
            var existingCard = _dbContext.Cards.FirstOrDefault(c => c.Id == Id && c.CreatorId == userId);

            if (existingCard == null)
                return NotFound("Card not found.");

            existingCard.DisplayName = updatedCard.DisplayName;
            existingCard.JobTitle = updatedCard.JobTitle;
            existingCard.Phone = updatedCard.Phone;
            existingCard.Mail = updatedCard.Mail;
            existingCard.WebsiteUrl = updatedCard.WebsiteUrl;
            existingCard.Address = updatedCard.Address;
            existingCard.WebsiteUrl = updatedCard.WebsiteUrl;
            existingCard.CreatedDate = updatedCard.CreatedDate;
            existingCard.IsActive = updatedCard.IsActive;

            _dbContext.SaveChanges();

            return Ok(existingCard);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }

    [HttpDelete("deleteCard/{Id}")]
    public IActionResult DeleteCard(String userId, int Id)
    {
        try
        {
            var cardToDelete = _dbContext.Cards.FirstOrDefault(c => c.Id == Id && c.CreatorId == userId);

            if (cardToDelete == null)
                return NotFound("Card not found.");

            _dbContext.Cards.Remove(cardToDelete);
            _dbContext.SaveChanges();

            return Ok($"Card with ID {Id} has been deleted.");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }

    [HttpPost("SaveCard")]
    public IActionResult SaveCard([FromBody] SavedModel model)
    {

        var cardExists = _dbContext.Cards.Any(c => c.Id == model.CardId);
        if (!cardExists)
        {
            return BadRequest("Card not found.");
        }

        // Check if the card is already saved
        var savedCardExists = _dbContext.SavedCards.Any(sc => sc.UserId == model.UserId && sc.CardId == model.CardId);
        if (savedCardExists)
        {
            return BadRequest("Card is already saved.");
        }

        try
        {
            var savedCard = new SavedModel { UserId = model.UserId, CardId = model.CardId };
            _dbContext.SavedCards.Add(savedCard);
            _dbContext.SaveChanges();
            return Ok(savedCard);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }

    //[HttpPost("SaveCard")]
    //public IActionResult SaveCard(string userId, int cardId)
    //{

    //    var cardExists = _dbContext.Cards.Any(c => c.Id == cardId);
    //    if (!cardExists)
    //    {
    //        return BadRequest("Card not found.");
    //    }

    //    try
    //    {
    //        var savedCard = new SavedModel { UserId = userId, CardId = cardId };
    //        _dbContext.SavedCards.Add(savedCard);
    //        _dbContext.SaveChanges();
    //        return Ok(savedCard);
    //    }
    //    catch (Exception ex)
    //    {
    //        return BadRequest($"Error: {ex.Message}");
    //    }
    //}

    [HttpGet("GetSavedCards/{userId}")]
    public IActionResult GetSavedCards(string userId)
    {
        try
        {
            var savedCards = _dbContext.SavedCards
                .Where(s => s.UserId == userId)
                .ToList();

            return Ok(savedCards);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }
}
