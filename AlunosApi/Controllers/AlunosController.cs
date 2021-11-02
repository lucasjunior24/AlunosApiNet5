using AlunosApi.Models;
using AlunosApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlunosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        private IAlunoService _alunoService;

        public AlunosController(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAlunos()
        {
            try
            {
                var alunos = await _alunoService.GetAlunos();
                return Ok(alunos);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error ao obter alunos!");
            }
        }

        [HttpGet("AlunosPorNome")]
        public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAlunosByNome([FromQuery]string nome)
        {
            try
            {
                var alunos = await _alunoService.GetAlunosByNome(nome);
                
                if (alunos.Count() == 0) return NotFound($"Não existem alunos com o critério {nome}");

                return Ok(alunos);
            }
            catch
            {
                return BadRequest("Error: Request Inválido!");
            }
        }

        [HttpGet("{id:int}", Name ="GetAluno")]
        public async Task<ActionResult<Aluno>> GetAluno( int id)
        {
            try
            {
                var aluno = await _alunoService.GetAluno(id);

                if (aluno == null) return NotFound($"Não existem aluno com id = {id}");

                return Ok(aluno);
            }
            catch
            {
                return BadRequest("Request Inválido!");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(Aluno aluno)
        {
            try
            {
                await _alunoService.CreateAluno(aluno);
                return CreatedAtRoute(nameof(GetAluno), new { id = aluno.Id }, aluno);
            }
            catch
            {
                return BadRequest("Request Inválido!");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Edit(int id, [FromBody] Aluno aluno)
        {
            try
            {
                if(id == aluno.Id)
                {
                    await _alunoService.UpdateAluno(aluno);
                    return Ok($"Aluno com id = {id} foi atualizado com sucesso!");
                }
                else
                {
                    return BadRequest("Dados inconsistentes!");
                }
            }
            catch
            {
                return BadRequest("Request Inválido!");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var aluno = await _alunoService.GetAluno(id);
                if(aluno != null)
                {
                    await _alunoService.DeleteAluno(aluno);
                    return Ok($"Aluno com id = {id} foi Excluido com sucesso!");
                }
                else
                {
                    return BadRequest("Aluno não encontrado!");
                }
            }
            catch
            {
                return BadRequest("Request Inválido!");
            }
        }
    }
}
