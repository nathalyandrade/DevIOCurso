﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DevIO.App.Data;
using DevIO.App.ViewModels;
using DevIO.Business.Interfaces;
using AutoMapper;
using DevIO.Business.Models;

namespace DevIO.App.Controllers
{
    public class FornecedoresController : BaseController
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IMapper _mapper;
        public FornecedoresController(IFornecedorRepository fornecedorRepository, IMapper mapper)
        {
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
              return View(_mapper.Map<IEnumerable<FornecedorViewModel>> (await _fornecedorRepository.ObterTodos()));
        }

        //[AllowAnonymous]
        //[Route("dados-do-fornecedor/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var fornecedorViewModel = await ObterFornecedorEndereco(id);

            if (fornecedorViewModel == null)
            {
                return NotFound();
            }

            return View(fornecedorViewModel);
        }

        //[ClaimsAuthorize("Fornecedor", "Adicionar")]
        //[Route("novo-fornecedor")]
        public IActionResult Create()
        {
            return View();
        }

        //[ClaimsAuthorize("Fornecedor", "Adicionar")]
        //[Route("novo-fornecedor")]
        [HttpPost]
        public async Task<IActionResult> Create(FornecedorViewModel fornecedorViewModel)
        {
            if (!ModelState.IsValid) return View(fornecedorViewModel);

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);
            await _fornecedorRepository.Adicionar(fornecedor);

            //if (!OperacaoValida()) return View(fornecedorViewModel);

            return RedirectToAction("Index");
        }

        //[ClaimsAuthorize("Fornecedor", "Editar")]
        //[Route("editar-fornecedor/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var fornecedorViewModel = await ObterFornecedorProdutosEndereco(id);

            if (fornecedorViewModel == null)
            {
                return NotFound();
            }

            return View(fornecedorViewModel);
        }

        //[ClaimsAuthorize("Fornecedor", "Editar")]
        //[Route("editar-fornecedor/{id:guid}")]
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, FornecedorViewModel fornecedorViewModel)
        {
            if (id != fornecedorViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(fornecedorViewModel);

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);
            await _fornecedorRepository.Atualizar(fornecedor);

            //if (!OperacaoValida()) return View(await ObterFornecedorProdutosEndereco(id));

            return RedirectToAction("Index");
        }

        //[ClaimsAuthorize("Fornecedor", "Excluir")]
        //[Route("excluir-fornecedor/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var fornecedorViewModel = await ObterFornecedorEndereco(id);

            if (fornecedorViewModel == null)
            {
                return NotFound();
            }

            return View(fornecedorViewModel);
        }

        //[ClaimsAuthorize("Fornecedor", "Excluir")]
        //[Route("excluir-fornecedor/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var fornecedor = await ObterFornecedorEndereco(id);

            if (fornecedor == null) return NotFound();

            await _fornecedorRepository.Remover(id);

            //if (!OperacaoValida()) return View(fornecedor);

            return RedirectToAction("Index");
        }

        //[AllowAnonymous]
        //[Route("obter-endereco-fornecedor/{id:guid}")]
        //public async Task<IActionResult> ObterEndereco(Guid id)
        //{
        //    var fornecedor = await ObterFornecedorEndereco(id);

        //    if (fornecedor == null)
        //    {
        //        return NotFound();
        //    }

        //    return PartialView("_DetalhesEndereco", fornecedor);
        //}

        //[ClaimsAuthorize("Fornecedor", "Editar")]
        //[Route("atualizar-endereco-fornecedor/{id:guid}")]
        //public async Task<IActionResult> AtualizarEndereco(Guid id)
        //{
        //    var fornecedor = await ObterFornecedorEndereco(id);

        //    if (fornecedor == null)
        //    {
        //        return NotFound();
        //    }

        //    return PartialView("_AtualizarEndereco", new FornecedorViewModel { Endereco = fornecedor.Endereco });
        //}

        //[ClaimsAuthorize("Fornecedor", "Editar")]
        //[Route("atualizar-endereco-fornecedor/{id:guid}")]
        //[HttpPost]
        //public async Task<IActionResult> AtualizarEndereco(FornecedorViewModel fornecedorViewModel)
        //{
        //    ModelState.Remove("Nome");
        //    ModelState.Remove("Documento");

        //    if (!ModelState.IsValid) return PartialView("_AtualizarEndereco", fornecedorViewModel);

        //    await _fornecedorService.AtualizarEndereco(_mapper.Map<Endereco>(fornecedorViewModel.Endereco));

        //    if (!OperacaoValida()) return PartialView("_AtualizarEndereco", fornecedorViewModel);

        //    var url = Url.Action("ObterEndereco", "Fornecedores", new { id = fornecedorViewModel.Endereco.FornecedorId });
        //    return Json(new { success = true, url });
        //}

        private async Task<FornecedorViewModel> ObterFornecedorEndereco(Guid id)
        {
            return _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorEndereco(id));
        }

        private async Task<FornecedorViewModel> ObterFornecedorProdutosEndereco(Guid id)
        {
            return _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorProdutosEndereco(id));
        }
    }
}
