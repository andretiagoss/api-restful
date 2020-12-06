using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using WebAPI.Models;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidosController(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        #region Pedidos

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult Obter()
        {
            try
            {
                var lista = _pedidoRepository.Obter();

                if (lista.Count() == 0)
                    return NotFound();

                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult Obter(Guid id)
        {
            try
            {
                var pedido = _pedidoRepository.Obter(id);

                if (pedido == null) return NotFound();

                return Ok(pedido);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Criar([FromBody] Pedido pedido)
        {
            try
            {
                if (pedido.Id == null)
                    return BadRequest("Codigo do pedido não informado!");

                _pedidoRepository.Criar(pedido);

                return Created(nameof(Criar), pedido);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult Atualizar([FromBody] Pedido pedido)
        {
            try
            {
                if (pedido.Id == null)
                    return BadRequest("Codigo do pedido não informado!");

                var _pedido = _pedidoRepository.Obter(pedido.Id);

                if (_pedido == null)
                {
                    _pedidoRepository.Criar(pedido);
                    return Created(nameof(Atualizar), pedido);
                }
                else
                {
                    _pedidoRepository.Atualizar(_pedido);
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public IActionResult Deletar(Guid id)
        {
            try
            {
                var pedido = _pedidoRepository.Obter(id);

                if (pedido == null) NotFound();

                _pedidoRepository.Deletar(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        #endregion

        #region Pedido Itens

        [HttpGet("{id}/item")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult ObterItem(Guid id)
        {
            try
            {
                var pedido = _pedidoRepository.Obter(id);

                if (pedido == null) return NotFound();

                return Ok(pedido.PedidoItems);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}/item/{itemId}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult ObterItem(Guid id, Guid itemId)
        {
            try
            {
                var pedido = _pedidoRepository.Obter(id);

                if (pedido == null) 
                    return NotFound();

                var item = pedido.PedidoItems.FirstOrDefault(a => a.Id == itemId);

                if (item == null)
                {
                    //return NotFound();
                    item = new PedidoItem(Guid.NewGuid(), "Produto 1", 1, new decimal(1.99));
                }
                    
                return Ok(item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}/item")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult AdicionarItem(Guid id, [FromBody] PedidoItem item)
        {
            try
            {
                if (id == null)
                    return BadRequest("Codigo do pedido não informado!");

                if (item.Id == null)
                    return BadRequest("Codigo do item não informado!");

                var pedido = _pedidoRepository.Obter(id);

                if (pedido == null)
                    return NotFound();

                var itemExistente = pedido.PedidoItems.FirstOrDefault(a => a.Id == item.Id);

                if (itemExistente == null)
                {
                    pedido.AdicionarItem(item);
                    _pedidoRepository.AdicionarItem(item);
                    
                    return Created(nameof(AdicionarItem), item);
                }
                else
                {
                    pedido.AtualizarItem(item);
                    _pedidoRepository.AtualizarItem(item);

                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}/item")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public IActionResult DeletarItem(Guid id, [FromBody] PedidoItem item)
        {
            try
            {
                if (id == null)
                    return BadRequest("Codigo do pedido não informado!");

                if (item.Id == null)
                    return BadRequest("Codigo do item não informado!");

                var pedido = _pedidoRepository.Obter(id);

                if (pedido == null)
                    return NotFound();

                var itemExistente = pedido.PedidoItems.FirstOrDefault(a => a.Id == item.Id);

                if (itemExistente == null)
                {
                    return NotFound();
                }
                else
                {
                    pedido.RemoverItem(item);
                    _pedidoRepository.DeletarItem(item);

                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        #endregion
    }
}
