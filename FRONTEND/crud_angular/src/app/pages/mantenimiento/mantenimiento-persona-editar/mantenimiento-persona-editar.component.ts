import {
  ChangeDetectionStrategy,
  Component,
  effect,
  inject,
  input,
  output,
} from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { PersonaDto } from '../../../models/persona/PersonaDto.model';
import { PersonaService } from '../../../services/persona/persona.service';

@Component({
  selector: 'app-mantenimiento-persona-editar',
  imports: [ReactiveFormsModule],
  templateUrl: './mantenimiento-persona-editar.component.html',
  styleUrls: ['./mantenimiento-persona-editar.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MantenimientoPersonaEditarComponent {
  persona = input<PersonaDto | null>(null);
  modo = input<'crear' | 'editar'>('crear');

  cancelado = output<void>();
  guardado = output<void>();

  private readonly personaService = inject(PersonaService);
  private readonly formBuilder = inject(FormBuilder);

  readonly form = this.formBuilder.group({
    nombres: ['', [Validators.required]],
    apellidoPaterno: ['', [Validators.required]],
    apellidoMaterno: [''],
    direccion: [''],
    telefono: [''],
  });

  cargando = false;

  constructor() {
    effect(() => {
      const persona = this.persona();

      this.form.reset({
        nombres: persona?.nombres ?? '',
        apellidoPaterno: persona?.apellidoPaterno ?? '',
        apellidoMaterno: persona?.apellidoMaterno ?? '',
        direccion: persona?.direccion ?? '',
        telefono: persona?.telefono ?? '',
      });
    });
  }

  onCancelar(): void {
    this.cancelado.emit();
  }

  onGuardar(): void {
    this.form.markAllAsTouched();
    if (this.form.invalid || this.cargando) {
      return;
    }

    this.cargando = true;

    const valores = this.form.getRawValue();
    const actual = this.persona();

    const payload: PersonaDto = {
      id: actual?.id ?? 0,
      idTipoDocumento: actual?.idTipoDocumento ?? 0,
      nombres: valores.nombres,
      apellidoPaterno: valores.apellidoPaterno,
      apellidoMaterno: valores.apellidoMaterno,
      direccion: valores.direccion,
      telefono: valores.telefono,
      userCreate: actual?.userCreate ?? 0,
      userUpdate: actual?.userUpdate ?? 0,
      dateCreated: actual?.dateCreated ?? '',
      dateUpdate: actual?.dateUpdate ?? '',
    };

    const request$ = this.modo() === 'editar' && payload.id > 0
      ? this.personaService.update(payload.id, payload)
      : this.personaService.create(payload);

    request$.subscribe({
      next: () => {
        this.guardado.emit();
      },
      error: (error) => {
        console.error('Error al guardar persona', error);
        this.cargando = false;
      },
      complete: () => {
        this.cargando = false;
      },
    });
  }
}
