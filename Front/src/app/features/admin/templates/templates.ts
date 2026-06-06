import { Component, OnInit, inject } from '@angular/core';
import { TemplateService } from '../../../core/services/template.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { CourseService } from '../../../core/services/course.service';
import { environment } from '../../../enviroments/environment';

@Component({
  selector: 'app-templates',
  standalone: true,
  imports: [CommonModule,
    FormsModule,
    DragDropModule],
  templateUrl: './templates.html'
})
export class Templates implements OnInit {

  private templateService = inject(TemplateService);
  private courseService = inject(CourseService);
  private apiUrl = environment.apiUrl;

  templates: any[] = [];
  filteredTemplates: any[] = [];
  courses: any[] = [];

  searchText: string = '';

  editingTemplateId: number | null = null;

  editModel = {
    idTemplate: 0,
    idCourse: 0,
    html: '',
    image: ''
  };

  createModel = {
    idCourse: 0,
    html: '',
    image: ''
  };

  backgroundImage = '';

  showPreviewModal = false;

  previewHtml = '';
  previewImage = '';

  designerFields: any[] = [];

  availableFields = [
    'TIPODOCUMENTO',
    'NUMERODOCUMENTO',
    'NOMBRE',
    'REGISTRO',
    'VIGENCIA',
    'CODIGOQR'
  ];

  selectedFile: File | null = null;

  ngOnInit(): void {
    this.loadTemplates();
     this.loadCourses();
  }

  showCreateModal = false;

  newTemplate(): void {

    this.showCreateModal = true;

  }

  closeModal(): void {

    this.showCreateModal = false;

  }

  saveNewTemplate(): void {

    if (!this.selectedFile) {

    alert('Debe seleccionar una imagen');
    return;

  }

  this.templateService
    .uploadImage(this.selectedFile)
    .subscribe({

      next: (response) => {

        console.log(response);
        this.createTemplate(response.message.url);

      },

      error: (err) => {

        console.error(err);

      }

    });
  }

  private createTemplate(imageUrl: string): void {

    this.createModel.html =
      this.generateHtml();

    this.createModel.image =
      imageUrl;

    const request = {

      IdCourse: this.createModel.idCourse,

      Html: this.createModel.html,

      Imagen: imageUrl

    };

    this.templateService
      .createTemplate(request)
      .subscribe({

        next: () => {

          this.showCreateModal = false;

          this.loadTemplates();

          alert('Plantilla creada exitosamente.');

        },

        error: err => {

          console.error(err);

        }

      });

  }

  loadTemplates(): void {

    this.templateService.getTemplates().subscribe({
      next: (response) => {

        this.templates = response.message;
        console.log(this.templates);
        this.filteredTemplates = [...this.templates];

      },
      error: (err) => {

        console.error(err);

      }
    });

  }

  loadCourses(): void {

    this.courseService.getCourses()
      .subscribe({
        next: (response) => {

          this.courses = response.message;

        },
        error: (err) => {

          console.error(err);

        }
      });

  }

  filterTemplate(): void {

    const search = this.searchText.toLowerCase().trim();

    if (!search) {

      this.filteredTemplates = [...this.templates];
      return;

    }

    this.filteredTemplates = this.templates.filter(template =>
      template.curso?.codigo?.toLowerCase().includes(search)
    );

  }

  editTemplate(template: any): void {

    this.editingTemplateId = template.id;

    this.editModel = {
      idTemplate: template.id,
      idCourse: template.cursoId,
      html: template.html,
      image: template.imagen
    };

  }

  cancelEdit(): void {

    this.editingTemplateId = null;

  }

  saveTemplate(): void {

    const request = {
      IdCourse: this.editModel.idCourse,
      Html: this.editModel.html,
      Imagen: this.editModel.image
    };

    this.templateService.updateTemplate(request)
      .subscribe({
        next: () => {

          this.editingTemplateId = null;

          this.loadTemplates();

        },
        error: err => {

          console.error(err);

        }
      });
  }

  deleteTemplate(id: number): void {

  if (!confirm('¿Desea eliminar este curso?')) {
    return;
  }

    this.templateService.deleteTemplate(id)
      .subscribe({
        next: () => {

          alert('Curso eliminado');

          this.loadTemplates();

        },
        error: err => {

          console.error(err);

        }
      });
  }

  preventInvalidKeys(event: KeyboardEvent): void {

    const invalidKeys = ['e', 'E', '+', '-'];

    if (invalidKeys.includes(event.key)) {
      event.preventDefault();
    }

  }

  onBackgroundSelected(event: any): void {

    const file = event.target.files[0];

    if (!file) {
      return;
    }

    this.selectedFile = file;

    const reader = new FileReader();

    reader.onload = () => {

      this.backgroundImage = reader.result as string;

    };

    reader.readAsDataURL(file);

  }

  addField(type: string): void {

    this.designerFields.push({
      type,
      x: 100,
      y: 100
    });

  }

  dragEnded(event: any, field: any): void {

    const position =
      event.source.getFreeDragPosition();

    field.x = position.x;
    field.y = position.y;

  }

  generateHtml(): string {

    let html = `
    <div
      style="
        position:relative;
        width:1200px;
        height:800px;
        overflow:hidden;
      ">

      <img
        src="{{IMAGEN}}"
        style="
          position:absolute;
          width:100%;
          height:100%;
          object-fit:cover;
        ">
    `;

    this.designerFields.forEach(field => {

      html += `
        <div
          style="
            position:absolute;
            left:${field.x}px;
            top:${field.y}px;
          ">
          {{${field.type}}}
        </div>
      `;

    });

    html += '</div>';

    return html;

  }

  previewTemplate(template: any): void {

    this.previewImage = this.apiUrl + template.imagen;

    this.previewHtml = template.html.replace(
    '{{IMAGEN}}',
    this.previewImage
    );

    this.showPreviewModal = true;

  }

}
