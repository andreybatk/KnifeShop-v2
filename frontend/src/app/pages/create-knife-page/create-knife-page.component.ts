import { Component, inject } from '@angular/core';
import { KnifeService } from '../../data/services/knife.service';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-knife-page',
  imports: [ReactiveFormsModule],
  templateUrl: './create-knife-page.component.html',
  styleUrl: './create-knife-page.component.scss'
})
export class CreateKnifePageComponent {
  knifeService = inject(KnifeService)
  router = inject(Router)

  form = new FormGroup({
    title: new FormControl(null, Validators.required),
    category: new FormControl(null, Validators.required),
    price: new FormControl(null, [Validators.required, Validators.min(0)]),
    description: new FormControl(null),
    isOnSale: new FormControl(false),
    image: new FormControl(null),
    images: new FormControl(null),
    knifeInfo: new FormGroup({
      overallLength: new FormControl(null),
      bladeLength: new FormControl(null),
      buttThickness: new FormControl(null),
      weight: new FormControl(null),
      handleMaterial: new FormControl(null),
      country: new FormControl(null),
      manufacturer: new FormControl(null),
      steelGrade: new FormControl(null),
    })
  });

  onSubmit() {
    if (this.form.valid) {
      //@ts-ignore
      this.knifeService.createKnife(this.form.value).subscribe(res => {
        console.log(res)
        this.router.navigate([''])
      })
    }
  }
}
