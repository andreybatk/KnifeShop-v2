import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { KnifeService } from '../../../data/services/knife.service';
import { Knife } from '../../../data/interfaces/knife.interface';
import { FileValidationService } from '../../../data/services/file-validation.service';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ImgUrlPipe } from '../../../helpers/pipes/img-url.pipe';
import { Category } from '../../../data/interfaces/category.interface';
import { NgSelectModule } from '@ng-select/ng-select';
import { CategoryService } from '../../../data/services/category.service';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-edit-knife-page',
  imports: [ReactiveFormsModule, CommonModule, ImgUrlPipe, NgSelectModule],
  templateUrl: './edit-knife-page.component.html',
  styleUrl: './edit-knife-page.component.scss'
})

export class EditKnifePageComponent implements OnInit {
  route = inject(ActivatedRoute)
  router = inject(Router)
  knifeService = inject(KnifeService)
  fileValidation = inject(FileValidationService)
  categoryService = inject(CategoryService)

  allCategories: Category[] = [];
  knife:Knife | null = null;
  id:number | null = null;

  ngOnInit() {
    this.id = Number(this.route.snapshot.paramMap.get('id'));
    if (!this.id) return;

    forkJoin({
      knife: this.knifeService.getKnife(this.id),
      categories: this.categoryService.getCategories()
    }).subscribe(({ knife, categories }) => {
      this.knife = knife;
      this.allCategories = categories;

      this.form.patchValue({
        id: knife.id,
        title: knife.title,
        categoryIds: knife.categories.map(x => x.id),
        price: knife.price,
        description: knife.description,
        isOnSale: knife.isOnSale,
        knifeInfo: {
          overallLength: knife.knifeInfo?.overallLength,
          bladeLength: knife.knifeInfo?.bladeLength,
          buttThickness: knife.knifeInfo?.buttThickness,
          weight: knife.knifeInfo?.weight,
          handleMaterial: knife.knifeInfo?.handleMaterial,
          country: knife.knifeInfo?.country,
          manufacturer: knife.knifeInfo?.manufacturer,
          steelGrade: knife.knifeInfo?.steelGrade,
        }
      });
    });
  }

  form = new FormGroup({
    id: new FormControl<number | null>(null, Validators.required),
    title: new FormControl<string | null>(null, Validators.required),
    categoryIds: new FormControl<number[]>([], Validators.required),
    price: new FormControl<number | null>(null, [Validators.required, Validators.min(0)]),
    description: new FormControl<string | null>(null),
    isOnSale: new FormControl<boolean | null>(true),
    image: new FormControl<File | null>(null, this.fileValidation.FileTypeValidator()),
    images: new FormControl<File[] | null>(null, this.fileValidation.FileTypeValidator()),
    knifeInfo: new FormGroup({
      overallLength: new FormControl<number | null>(null),
      bladeLength: new FormControl<number | null>(null),
      buttThickness: new FormControl<number | null>(null),
      weight: new FormControl<number | null>(null),
      handleMaterial: new FormControl<string | null>(null),
      country: new FormControl<string | null>(null),
      manufacturer: new FormControl<string | null>(null),
      steelGrade: new FormControl<string | null>(null),
    })
  });

  imageFile: File | null = null;
  imageFiles: File[] = [];

  onImageSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files[0]) {
      this.imageFile = input.files[0];
    }
  }

  onImagesSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files) {
      this.imageFiles = Array.from(input.files);
    }
  }

  onSubmit() {
    if (this.form.valid) {
      //@ts-ignore
      this.knifeService.editKnife(this.form.value, this.imageFile, this.imageFiles).subscribe(id => {
        this.router.navigate([`knife/${id}`])
      })
    }
  }

  onClickDelete() {
    const confirmed = window.confirm('Вы уверены, что хотите удалить нож?');
    if (confirmed) {
      //@ts-ignore
      this.knifeService.deleteKnife(this.id).subscribe(id => {
        this.router.navigate(['admin-panel']);
      });
    }
  }
}
