import { Component, OnInit, inject, ɵɵregisterNgModuleType } from '@angular/core';
import { CourseService } from '../../../core/services/course.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { environment } from '../../../enviroments/environment';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { TemplateService } from '../../../core/services/template.service';
import { MatTooltipModule } from '@angular/material/tooltip';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-courses',
  standalone: true,
  imports: [CommonModule, FormsModule, DragDropModule, MatTooltipModule],
  templateUrl: './courses.html',
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
  filteredCategories: any[] = [];
  filteredSubCategories: any[] = [];
  filteredCosts: any[] = [];

  searchText: string = '';

  editingCourseId: number | null = null;
  selectedFile: File | null = null;
  editingTemplateId: number | null = null;
  editingCategoryId: number | null = null;
  editingSubCategoryId: number | null = null;
  editingCostId: number | null = null;

  editModel = {
    idCourse: 0,
    code: '',
    name: '',
    description: '',
    validity: 0,
    idSubCategory: 0,
    idCost: 0
  };

  createModel = {
    code: '',
    name: '',
    description: '',
    validity: 0,
    idSubCategory: 0,
    idCost: 0
  };

  editModelTemplate = {
    idTemplate: 0,
    idCourse: 0,
    html: '',
    image: '',
  };

  createModelTemplate = {
    idCourse: 0,
    html: '',
    image: '',
  };

  createModelCategory = {
    nombre: '',
  };

  editModelCategory = {
    idCategoria: 0,
    nombre: '',
  };

  createModelSubCategory = {
    nombre: '',
    idCategory: 0,
  };

  editModelSubCategory = {
    idSubCategoria: 0,
    nombre: '',
    idCategory: 0,
  };

  createModelCost = {
    codigo: '',
    valor: 0,
  };

  editModelCost = {
    idCosto: 0,
    codigo: '',
    valor: 0,
  };

  availableFields = [
    'TIPODOCUMENTO',
    'NUMERODOCUMENTO',
    'NOMBRE',
    'REGISTRO',
    'VIGENCIA',
    'CODIGOQR',
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
  showNewAdminMenu = false;
  showCreateModal = false;
  showCreateTemplateModal = false;
  modalAdminVisible = false;
  modalNewAdminVisible = false;
  modalType = '';
  showModalType = '';

  //Mostrar modal de administracion de categorias
  openModalAdmin(type: string): void {
    this.modalType = type;
    this.modalAdminVisible = true;
    this.showAdminMenu = false;
    this.loadDataAdmin();
  }

  //Mostrar modal de creacion de categorias
  showModalAdminNew(type: string): void {
    this.modalType = type;
    this.modalNewAdminVisible = true;
    this.showNewAdminMenu = true;
    this.loadDataAdmin();
  }

  //Cerrar modal de administracion de categorias
  closeModalAdmin(): void {
    this.modalAdminVisible = false;
  }

  ngOnInit(): void {
    this.loadCourses();
    this.loadTemplates();
    this.modalType = 'costos';
    this.loadDataAdmin();
  }

  newTemplate(): void {
    this.availableCourses = this.courses.filter(
      (course) => !this.templates.some((template) => template.cursoId === course.id),
    );

    if (this.availableCourses.length === 0) {
      Swal.fire({
        icon: 'success',
        title: 'Éxito',
        text: 'No hay cursos disponibles para asignar plantilla.',
        confirmButtonColor: '#fea419',
      });

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
    this.showNewAdminMenu = false;
  }

  saveNewCourse(): void {
    console.log(this.createModel);

    const request = {
      CodeCourse: this.createModel.code,
      NameCourse: this.createModel.name,
      DescriptionCourse: this.createModel.description,
      Validity: this.createModel.validity,
      IdSubCategory: this.createModel.idSubCategory,
      IdCost: this.createModel.idCost,
    };

    this.courseService.createCourse(request).subscribe({
      next: () => {
        this.showCreateModal = false;
        this.loadCourses();

        Swal.fire({
          icon: 'success',
          title: 'Éxito',
          text: 'Curso creado exitosamente.',
          confirmButtonColor: '#fea419',
        });
      },
      error: (err) => {
        console.error(err);
      },
    });
  }

  saveNewAdmin(): void {
    if (this.modalType === 'categorias') {
      console.log(this.createModelCategory);

      const request = {
        NameCategory: this.createModelCategory.nombre,
      };

      this.courseService.createCategory(request).subscribe({
        next: () => {
          this.showNewAdminMenu = false;
          this.loadDataAdmin();

          Swal.fire({
            icon: 'success',
            title: 'Éxito',
            text: 'Categoria creada exitosamente.',
            confirmButtonColor: '#fea419',
          });
        },
        error: (err) => {
          Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Error al crear la categoria.',
            confirmButtonColor: '#fea419',
          });
        },
      });
    }

    if (this.modalType === 'subcategorias') {
      console.log(this.createModelSubCategory);

      const request = {
        NameSubCategory: this.createModelSubCategory.nombre,
        IdCategory: this.createModelSubCategory.idCategory,
      };

      this.courseService.createSubCategory(request).subscribe({
        next: () => {
          this.showNewAdminMenu = false;
          this.loadDataAdmin();

          Swal.fire({
            icon: 'success',
            title: 'Éxito',
            text: 'SubCategoria creada exitosamente.',
            confirmButtonColor: '#fea419',
          });
        },
        error: (err) => {
          Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Error al crear la subcategoria.',
            confirmButtonColor: '#fea419',
          });
        },
      });
    }

    if (this.modalType === 'costos') {
      console.log(this.createModelCost);

      const request = {
        CodeCost: this.createModelCost.codigo,
        ValueCost: this.createModelCost.valor,
      };

      this.courseService.createCost(request).subscribe({
        next: () => {
          this.showNewAdminMenu = false;
          this.loadDataAdmin();

          Swal.fire({
            icon: 'success',
            title: 'Éxito',
            text: 'Costo creado exitosamente.',
            confirmButtonColor: '#fea419',
          });
        },
        error: (err) => {
          Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Error al crear el costo.',
            confirmButtonColor: '#fea419',
          });
        },
      });
    }
  }

  loadCourses(): void {
    this.courseService.getCourses().subscribe({
      next: (response) => {
        this.courses = response.message;
        this.filteredCourses = [...this.courses];
      },
      error: (err) => {
        console.error(err);
      },
    });
  }

  loadDataAdmin(): void {
    if (this.modalType === 'categorias') {
      this.courseService.getCategories().subscribe({
        next: (response) => {
          this.categories = response.message;
          this.filteredCategories = [...this.categories];
        },
        error: (err) => {
          console.error(err);
        },
      });
    }

    if (this.modalType === 'subcategorias') {
      this.courseService.getCategories().subscribe({
        next: (response) => {
          this.categories = response.message;
          console.log(this.categories);
        },
        error: (err) => {
          console.error(err);
        },
      });

      this.courseService.getSubCategories().subscribe({
        next: (response) => {
          this.subcategories = response.message;
          this.filteredSubCategories = [...this.subcategories];
        },
        error: (err) => {
          console.error(err);
        },
      });
    }

    if (this.modalType === 'costos') {

      this.courseService.getSubCategories().subscribe({
        next: (response) => {
          this.subcategories = response.message;
        },
        error: (err) => {
          console.error(err);
        },
      });

      this.courseService.getCosts().subscribe({
        next: (response) => {
          this.costs = response.message;
          this.filteredCosts = [...this.costs];
        },
        error: (err) => {
          console.error(err);
        },
      });
    }
  }

  loadTemplates(): void {
    this.templateService.getTemplates().subscribe({
      next: (response) => {
        this.templates = response.message;
        console.log(this.templates);
      },
      error: (err) => {
        console.error(err);
      },
    });
  }

  filterCourse(): void {
    const search = this.searchText.toLowerCase().trim();

    if (!search) {
      this.filteredCourses = [...this.courses];
      return;
    }

    this.filteredCourses = this.courses.filter(
      (course) =>
        course.codigo?.toLowerCase().includes(search) ||
        course.nombre?.toLowerCase().includes(search) ||
        course.descripcion?.toLowerCase().includes(search) ||
        course.subCategoria?.nombre?.toLowerCase().includes(search),
    );
  }

  filterAdmin(): void {
    if (this.modalType === 'categorias') {
      const search = this.searchText.toLowerCase().trim();

      if (!search) {
        this.filteredCategories = [...this.categories];
        return;
      }

      this.filteredCategories = this.categories.filter((category) =>
        category.nombre?.toLowerCase().includes(search),
      );
    }

    if (this.modalType === 'subcategorias') {
      const search = this.searchText.toLowerCase().trim();

      if (!search) {
        this.filteredCategories = [...this.categories];
        return;
      }

      this.filteredCategories = this.categories.filter((category) =>
        category.nombre?.toLowerCase().includes(search),
      );
    }

    if (this.modalType === 'costos') {
      const search = this.searchText.toLowerCase().trim();

      if (!search) {
        this.filteredCategories = [...this.categories];
        return;
      }

      this.filteredCategories = this.categories.filter((category) =>
        category.nombre?.toLowerCase().includes(search),
      );
    }
  }

  editCourse(course: any): void {
    this.editingCourseId = course.id;

    this.editModel = {
      idCourse: course.id,
      code: course.codigo,
      name: course.nombre,
      description: course.descripcion,
      validity: course.vigencia,
      idCost: course.idCost,
      idSubCategory: course.idSubCategory
    };
  }

  editAdmin(adminId: any): void {
    if (this.modalType === 'categorias') {
      this.editingCategoryId = adminId.id;

      this.editModelCategory = {
        idCategoria: adminId.id,
        nombre: adminId.nombre,
      };
    }

    if (this.modalType === 'subcategorias') {
      this.editingSubCategoryId = adminId.id;

      this.editModelSubCategory = {
        idSubCategoria: adminId.id,
        nombre: adminId.nombre,
        idCategory: adminId.IdCategory,
      };
    }

    if (this.modalType === 'costos') {
      this.editingCostId = adminId.id;

      this.editModelCost = {
        idCosto: adminId.id,
        codigo: adminId.codigo,
        valor: adminId.valor,
      };
    }
  }

  cancelEdit(): void {
    this.editingCourseId = null;
    this.editingCategoryId = null;
    this.editingSubCategoryId = null;
    this.editingCostId = null;
  }

  saveCourse(): void {
    const request = {
      IdCourse: this.editModel.idCourse,
      CodeCourse: this.editModel.code,
      NameCourse: this.editModel.name,
      DescriptionCourse: this.editModel.description,
      Validity: this.editModel.validity,
      IdSubCategory: this.editModel.idCost,
      IdCost: this.editModel.idSubCategory
    };

    this.courseService.updateCourse(request).subscribe({
      next: () => {
        this.editingCourseId = null;

        this.loadCourses();
      },
      error: (err) => {
        console.error(err);
      },
    });
  }

  saveEditAdmin(): void {
    if (this.modalType === 'categorias') {
      const request = {
        IdCategory: this.editModelCategory.idCategoria,
        NameCategory: this.editModelCategory.nombre,
      };

      this.courseService.updateCategory(request).subscribe({
        next: () => {
          this.editingCategoryId = null;

          this.loadDataAdmin();
        },
        error: (err) => {
          console.error(err);
        },
      });
    }

    if (this.modalType === 'subcategorias') {
      const request = {
        IdSubCategory: this.editModelSubCategory.idSubCategoria,
        NameSubCategory: this.editModelSubCategory.nombre,
        IdCategory: this.editModelSubCategory.idCategory,
      };

      this.courseService.updateSubCategory(request).subscribe({
        next: () => {
          this.editingSubCategoryId = null;

          this.loadDataAdmin();
        },
        error: (err) => {
          console.error(err);
        },
      });
    }

    if (this.modalType === 'costos') {
      const request = {
        IdCost: this.editModelCost.idCosto,
        CodeCost: this.editModelCost.codigo,
        ValueCost: this.editModelCost.valor,
      };

      this.courseService.updateCost(request).subscribe({
        next: () => {
          this.editingCostId = null;

          this.loadDataAdmin();
        },
        error: (err) => {
          console.error(err);
        },
      });
    }
  }

  deleteCourse(id: number): void {
    if (!confirm('¿Desea eliminar este curso?')) {
      return;
    }

    this.templateService.getTemplates().subscribe({
      next: (response) => {
        const template = response.message.find((x: any) => x.cursoId === id);

        if (template) {
          this.templateService.deleteTemplate(template.id).subscribe({
            next: () => {
              this.courseService.deleteCourse(id).subscribe({
                next: () => {
                  alert('Curso eliminado');

                  this.loadCourses();
                },
                error: (err) => console.error(err),
              });
            },
            error: (err) => console.error(err),
          });
        }
      },
      error: (err) => console.error(err),
    });
  }

  deleteTemplate(id: number): void {
    this.templateService.deleteTemplate(id).subscribe({
      next: () => {},
      error: (err) => {
        console.error(err);
      },
    });
  }

  deleteAdmin(id: number): void {
    if (this.modalType === 'categorias') {
      Swal.fire({
        title: '¿Desea eliminar esta categoría?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Sí, eliminar',
        cancelButtonText: 'Cancelar',
      }).then((result) => {
        if (result.isConfirmed) {
          this.courseService.deleteCategory(id).subscribe({
            next: () => {
              Swal.fire('Eliminado', 'Categoria eliminada correctamente', 'success');

              this.loadDataAdmin();
            },
            error: (err) => {
              console.error(err);
            },
          });
        }
      });
    }

    if (this.modalType === 'subcategorias') {
      Swal.fire({
        title: '¿Desea eliminar esta subcategoría?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Sí, eliminar',
        cancelButtonText: 'Cancelar',
      }).then((result) => {
        if (result.isConfirmed) {
          this.courseService.deleteSubCategory(id).subscribe({
            next: () => {
              Swal.fire('Eliminado', 'Subategoria eliminada correctamente', 'success');

              this.loadDataAdmin();
            },
            error: (err) => {
              console.error(err);
            },
          });
        }
      });
    }

    if (this.modalType === 'costos') {
      Swal.fire({
        title: '¿Desea eliminar este costo?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Sí, eliminar',
        cancelButtonText: 'Cancelar',
      }).then((result) => {
        if (result.isConfirmed) {
          this.courseService.deleteCost(id).subscribe({
            next: () => {
              Swal.fire('Eliminado', 'Costo eliminado correctamente', 'success');

              this.loadDataAdmin();
            },
            error: (err) => {
              console.error(err);
            },
          });
        }
      });
    }
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

    this.templateService.uploadImage(this.selectedFile).subscribe({
      next: (response) => {
        console.log(response);
        this.createTemplate(response.message.url);
      },

      error: (err) => {
        console.error(err);
      },
    });
  }

  private createTemplate(imageUrl: string): void {
    this.createModelTemplate.html = this.generateHtml();

    this.createModelTemplate.image = imageUrl;

    const request = {
      IdCourse: this.createModelTemplate.idCourse,

      Html: this.createModelTemplate.html,

      Imagen: imageUrl,
    };

    this.templateService.createTemplate(request).subscribe({
      next: () => {
        this.showCreateTemplateModal = false;

        this.loadCourses();

        alert('Plantilla creada exitosamente.');
      },

      error: (err) => {
        console.error(err);
      },
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
      y: 100,
    });
  }

  dragEnded(event: any, field: any): void {
    const position = event.source.getFreeDragPosition();

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

    this.designerFields.forEach((field) => {
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
    const template = this.templates.find((x) => x.cursoId === course.id);

    if (!template) {
      alert('El curso no tiene plantilla asociada');
      return;
    }

    this.previewImage = this.apiUrl + template.imagen;

    this.previewHtml = template.html.replace('{{IMAGEN}}', this.previewImage);

    this.showPreviewModal = true;
  }

  editTemplate(course: any): void {
    const template = this.templates.find((x) => x.cursoId === course.id);

    if (!template) {
      alert('El curso no tiene plantilla');
      return;
    }

    this.isEditingTemplate = true;

    this.editingTemplateId = template.id;

    this.createModelTemplate = {
      idCourse: template.cursoId,
      html: template.html,
      image: template.imagen,
    };

    this.backgroundImage = this.apiUrl + template.imagen;

    this.loadFieldsFromHtml(template.html);

    this.showCreateTemplateModal = true;
  }

  loadFieldsFromHtml(html: string): void {
    this.designerFields = [];

    const regex = /left:(\d+)px;[\s\S]*?top:(\d+)px;[\s\S]*?\{\{(.*?)\}\}/g;

    let match;

    while ((match = regex.exec(html)) !== null) {
      this.designerFields.push({
        x: Number(match[1]),

        y: Number(match[2]),

        type: match[3],
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

      Imagen: this.createModelTemplate.image,
    };

    this.templateService.updateTemplate(request).subscribe({
      next: () => {
        alert('Plantilla actualizada');

        this.showCreateTemplateModal = false;

        this.isEditingTemplate = false;

        this.loadTemplates();
      },

      error: (err) => {
        console.error(err);
      },
    });
  }
}
