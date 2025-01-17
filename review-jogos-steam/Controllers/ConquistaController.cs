using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using review_jogos_steam.Models;
using review_jogos_steam.Data;

namespace review_jogos_steam.Controllers;

[ApiController]
[Route("[controller]")]
public class ConquistaController : ControllerBase
{
    private ReviewDbContext _dbContext;

    public ConquistaController(ReviewDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost()]
    [Route("cadastrar")]
    public async Task<ActionResult> Cadastrar(Conquista conquista)
    {
        if(_dbContext is null) return NotFound();
        if(_dbContext.Conquista is null) return NotFound();
        await _dbContext.AddAsync(conquista);
        await _dbContext.SaveChangesAsync();
        return Created("",conquista);
    }
    
    [HttpGet()]
    [Route("listar")]
    public async Task<ActionResult<IEnumerable<Conquista>>> Listar()
    {
        if(_dbContext is null) return NotFound();
        if(_dbContext.Conquista is null) return NotFound();
        return await _dbContext.Conquista.ToListAsync();
    }

    [HttpGet()]
    [Route("buscar/{idConquista}")]
    public async Task<ActionResult<Conquista>> Buscar(int idConquista)
    {
        if(_dbContext is null) return NotFound();
        if(_dbContext.Conquista is null) return NotFound();
        var conctemp = await _dbContext.Conquista.FindAsync(idConquista);
        if(conctemp is null) return NotFound();
        return conctemp;
    }

    [HttpPut()]
    [Route("alterar")]
    public async Task<ActionResult> Alterar(Conquista conquista)
    {
        if(_dbContext is null) return NotFound();
        if(_dbContext.Conquista is null) return NotFound();
        _dbContext.Conquista.Update(conquista);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete()]
    [Route("excluir/{idConquista}")]
    public async Task<ActionResult> Excluir(int idConquista)
    {
        if(_dbContext is null) return NotFound();
        if(_dbContext.Conquista is null) return NotFound();
        var conctemp = await _dbContext.Conquista.FindAsync(idConquista);
        if(conctemp is null) return NotFound();
        _dbContext.Conquista.Remove(conctemp);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
}