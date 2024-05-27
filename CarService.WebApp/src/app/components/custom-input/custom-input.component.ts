import { NgIf } from '@angular/common';
import { Component, Input, OnInit, forwardRef } from '@angular/core';
import {
  ControlValueAccessor,
  FormControl,
  NG_VALUE_ACCESSOR,
  ReactiveFormsModule,
} from '@angular/forms';

@Component({
  selector: 'custom-input',
  standalone: true,
  imports: [ReactiveFormsModule, NgIf],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => CustomInputComponent),
      multi: true,
    },
  ],
  templateUrl: './custom-input.component.html',
  styleUrl: './custom-input.component.scss',
})
export class CustomInputComponent implements ControlValueAccessor, OnInit {
  @Input({ required: true })
  type: 'text' | 'password' | 'email';

  @Input({ required: true })
  placeholder: string = '';

  @Input()
  formControl: FormControl;

  @Input()
  name?: string;

  @Input()
  title?: string;

  @Input()
  heightInput: string = '50px';

  isPasswordType: boolean;
  passwordShow: boolean = false;
  value: string;

  onChange: (value: string) => void = () => {};
  onTouched: () => void = () => {};

  ngOnInit(): void {
    this.isPasswordType = this.type === 'password';
  }

  convertEvent(event: any) {
    this.onChange(event.target.value);
  }

  writeValue(value: string): void {
    this.value = value;
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  togglePasswordShow(event: any) {
    this.type = this.type === 'password' ? 'text' : 'password';
    this.passwordShow = event.target.checked;
  }
}
