import { Component, inject, OnInit } from '@angular/core';
import { PersonaService } from '../../../services/persona/persona.service';
import { PersonaDto } from '../../../models/persona/PersonaDto.model';

@Component({
  selector: 'app-mantenimiento-persona-list',
  templateUrl: './mantenimiento-persona-list.component.html',
  styleUrls: ['./mantenimiento-persona-list.component.css']
})
export class MantenimientoPersonaListComponent implements OnInit {


  //INYECTAR LOS SERVICIOS HHTPCLIENT ==> PERSONA SERVICE

  _personaService = inject(PersonaService);

  personas: PersonaDto[]  = [];

  constructor() { }

  ngOnInit() {
    this.getAllPersonas();
  }

  getAllPersonas() {

    this._personaService.getAll().subscribe({
      //next => quiere decir que se ejecuta cuando la respuesta es exitosa
      next: (data) => {
        console.log("respuesta", data);
        this.personas = data;
      },
      //error => se ejecuta cuando hay un error en la respuesta
      error: (err) => { console.log("ocurrio un error", err); },
      //complete => se ejecuta cuando la respuesta se completa, ya sea exitosa o con error
      complete: () => { console.log('getAllPersonas completed'); }
    });

  }


}
