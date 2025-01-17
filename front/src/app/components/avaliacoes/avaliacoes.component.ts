import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { AvaliacoesService } from 'src/app/avaliacoes.service';
import { Avaliacao } from 'src/app/Avaliacao';
import { JogosService } from 'src/app/jogos.service';
import { Jogo } from 'src/app/Jogo';
import { Observer } from 'rxjs';

@Component({
  selector: 'app-avaliacoes',
  templateUrl: './avaliacoes.component.html',
  styleUrls: ['./avaliacoes.component.css']
})
export class AvaliacoesComponent implements OnInit {
  formularioAvaliacao: any;
  tituloFormulario: string = '';
  jogos: Array<Jogo> | undefined;
  constructor(private avaliacaoService : AvaliacoesService, private jogosService: JogosService) { }

  ngOnInit(): void {
    this.tituloFormulario = 'Nova Avaliação';
    
    this.jogosService.listar().subscribe(jogos => {
      this.jogos = jogos;
      if (this.jogos && this.jogos.length > 0){
        this.formularioAvaliacao.get('jogoId')?.setValue(this.jogos[0].idJogo);
      }
    });

    this.formularioAvaliacao = new FormGroup({
      jogoId: new FormControl(null),
      nota: new FormControl(null)
    });
  }
  enviarFormulario(): void {
    console.log('Método enviarFormulario() chamado.');
    const avaliacao: Avaliacao = this.formularioAvaliacao.value;
    const observer: Observer<Avaliacao> = {
      next(_result): void {
        alert('Modelo salvo com sucesso.');
      },
      error(_error): void {
        alert('Erro ao salvar!');
      },
      complete(): void {
      },
    };

    if (avaliacao.idAvaliacao && !isNaN(Number(avaliacao.idAvaliacao))) {
      this.avaliacaoService.alterar(avaliacao).subscribe(observer);
    } else {
      this.avaliacaoService.cadastrar(avaliacao).subscribe(observer);
    }
  }
}
