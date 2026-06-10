import { Component, OnInit, inject } from '@angular/core';
import { CourseService } from '../../../core/services/course.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { environment } from '../../../enviroments/environment';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { TemplateService } from '../../../core/services/template.service';
import { MatTooltipModule } from '@angular/material/tooltip';

@Component({
  selector: 'app-courses',
  standalone: true,
  imports: [CommonModule,FormsModule, DragDropModule,MatTooltipModule],
  templateUrl: './courses.html'
})
export class Courses implements OnInit {

  private templateService = inject(TemplateService);
  private courseService = inject(CourseService);
  private apiUrl = environment.apiUrl;

  templates: any[] = [];
  courses: any[] = [];
  categories: any[] = [];
  subcategories: any[] = [];
  costs: any[] = [];
  filteredCourses: any[] = [];

  searchText: string = '';

  editingCourseId: number | null = null;
  selectedFile: File | null = null;
  editingTemplateId: number | null = null;

  editModel = {
    idCourse: 0,
    code: '',
    name: '',
    description: '',
    validity : 0,
    cost: 0
  };

  createModel = {
    code: '',
    name: '',
    description: '',
    validity : 0,
    cost: 0
  };

  editModelTemplate = {
    idTemplate: 0,
    idCourse: 0,
    html: '',
    image: ''
  };

  createModelTemplate = {
    idCourse: 0,
    html: '',
    image: ''
  };

  availableFields = [
    'TIPODOCUMENTO',
    'NUMERODOCUMENTO',
    'NOMBRE',
    'REGISTRO',
    'VIGENCIA',
    'CODIGOQR'
  ];

  showPreviewModal = false;
  previewHtml = '';
  previewImage = '';
  backgroundImage = '';
  designerFields: any[] = [];
  idTemplate = 0;
  availableCourses: any[] = [];
  isEditingTemplate = false;

  showAdminMenu = false;

  modalAdminVisible = false;
  modalType = '';

  openModalAdmin(type: string): void {

    this.modalType = type;
    this.modalAdminVisible = true;
    this.showAdminMenu = false;

  }

  closeModalAdmin(): void {

    this.modalAdminVisible = false;

  }

  ngOnInit(): void {
    this.loadCourses();
    this.loadTemplates();
  }

  showCreateModal = false;

  showCreateTemplateModal = false;

  newTemplate(): void {

    this.availableCourses = this.courses.filter(course =>
      !this.templates.some(
        template => template.cursoId === course.id
      )
    );

    if (this.availableCourses.length === 0) {

      alert(
        'No hay cursos disponibles para asignar plantilla.'
      );

      return;

    }

    this.showCreateTemplateModal = true;

  }

  closeModalTemplate(): void {

    this.showCreateTemplateModal = false;

  }

  newCourse(): void {

    this.showCreateModal = true;

  }

  closeModal(): void {

    this.showCreateModal = false;

  }

  saveNewCourse(): void {

  console.log(this.createModel);

  const request = {
      Code: this.createModel.code,
      Name: this.createModel.name,
      Description: this.createModel.description,
      Validity: this.createModel.validity,
      Cost: this.createModel.cost
      };

    this.courseService.createCourse(request)
      .subscribe({
        next: () => {

          this.showCreateModal = false;
          this.loadCourses();
          alert("Curso creado exitosamente.");

        },
        error: err => {

          console.error(err);

        }
      });



  }

  loadCourses(): void {

    this.courseService.getCourses().subscribe({
      next: (response) => {

        this.courses = response.message;
        this.filteredCourses = [...this.courses];

      },
      error: (err) => {

        console.error(err);

      }
    });

  }

  loadTemplates(): void {

    this.templateService.getTemplates().subscribe({
      next: (response) => {

        this.templates = response.message;
        console.log(this.templates);
      },
      error: (err) => {

        console.error(err);

      }
    });

  }

  filterCourse(): void {

    const search = this.searchText.toLowerCase().trim();

    if (!search) {

      this.filteredCourses = [...this.courses];
      return;

    }

    this.filteredCourses = this.courses.filter(course =>
      course.codigo?.toLowerCase().includes(search) ||
      course.nombre?.toLowerCase().includes(search) ||
      course.descripcion?.toLowerCase().includes(search) ||
      course.subCategoria?.nombre?.toLowerCase().includes(search)
    );

  }

  editCourse(course: any): void {

    this.editingCourseId = course.id;

    this.editModel = {
      idCourse: course.id,
      code: course.codigo,
      name: course.nombre,
      description: course.descripcion,
      validity: course.vigencia,
      cost: course.costo
    };

  }

  cancelEdit(): void {

    this.editingCourseId = null;

  }

  saveCourse(): void {

    const request = {
      IdCourse: this.editModel.idCourse,
      Code: this.editModel.code,
      Name: this.editModel.name,
      Description: this.editModel.description,
      Validity: this.editModel.validity,
      Cost: this.editModel.cost
    };

    this.courseService.updateCourse(request)
      .subscribe({
        next: () => {

          this.editingCourseId = null;

          this.loadCourses();

        },
        error: err => {

          console.error(err);

        }
      });
  }

  deleteCourse(id: number): void {

    if (!confirm('¿Desea eliminar este curso?')) {
      return;
    }

    this.templateService.getTemplates()
      .subscribe({
        next: (response) => {

          const template = response.message.find(
            (x: any) => x.cursoId === id
          );

          if (template) {

            this.templateService.deleteTemplate(template.id)
              .subscribe({
                next: () => {

                  this.courseService.deleteCourse(id)
                    .subscribe({
                      next: () => {

                        alert('Curso eliminado');

                        this.loadCourses();

                      },
                      error: err => console.error(err)
                    });

                },
                error: err => console.error(err)
              });

          }

        },
        error: err => console.error(err)
      });

  }

  deleteTemplate(id: number): void {

    this.templateService.deleteTemplate(id)
      .subscribe({
        next: () => {
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

    this.createModelTemplate.html =
      this.generateHtml();

    this.createModelTemplate.image =
      imageUrl;

    const request = {

      IdCourse: this.createModelTemplate.idCourse,

      Html: this.createModelTemplate.html,

      Imagen: imageUrl

    };

    this.templateService
      .createTemplate(request)
      .subscribe({

        next: () => {

          this.showCreateTemplateModal = false;

          this.loadCourses();

          alert('Plantilla creada exitosamente.');

        },

        error: err => {

          console.error(err);

        }

      });

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

  previewTemplate(course: any): void {

    const template = this.templates.find(
      x => x.cursoId === course.id
    );

    if (!template) {

      alert('El curso no tiene plantilla asociada');
      return;

    }

    this.previewImage = this.apiUrl + template.imagen;

    this.previewHtml = template.html.replace(
      '{{IMAGEN}}',
      this.previewImage
    );

    this.showPreviewModal = true;

  }

  editTemplate(course: any): void {

    const template = this.templates.find(
      x => x.cursoId === course.id
    );

    if (!template) {

      alert('El curso no tiene plantilla');
      return;

    }

    this.isEditingTemplate = true;

    this.editingTemplateId = template.id;

    this.createModelTemplate = {

      idCourse: template.cursoId,
      html: template.html,
      image: template.imagen

    };

    this.backgroundImage =
      this.apiUrl + template.imagen;

    this.loadFieldsFromHtml(template.html);

    this.showCreateTemplateModal = true;

  }

  loadFieldsFromHtml(html: string): void {

    this.designerFields = [];

    const regex =
      /left:(\d+)px;[\s\S]*?top:(\d+)px;[\s\S]*?\{\{(.*?)\}\}/g;

    let match;

    while ((match = regex.exec(html)) !== null) {

      this.designerFields.push({

        x: Number(match[1]),

        y: Number(match[2]),

        type: match[3]

      });

    }

  }

  saveTemplate(): void {

  if (this.isEditingTemplate) {

    this.updateTemplate();

  } else {

    this.saveNewTemplate();

  }

}

updateTemplate(): void {

  const request = {

    IdTemplate: this.editingTemplateId,

    IdCourse: this.createModelTemplate.idCourse,

    Html: this.generateHtml(),

    Imagen: this.createModelTemplate.image

  };

  this.templateService
    .updateTemplate(request)
    .subscribe({

      next: () => {

        alert('Plantilla actualizada');

        this.showCreateTemplateModal = false;

        this.isEditingTemplate = false;

        this.loadTemplates();

      },

      error: err => {

        console.error(err);

      }

    });

}

}
