/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { DaneComponent } from './dane.component';

describe('DaneComponent', () => {
  let component: DaneComponent;
  let fixture: ComponentFixture<DaneComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DaneComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DaneComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
