import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OwntitlesComponent } from './owntitles.component';

describe('OwntitlesComponent', () => {
  let component: OwntitlesComponent;
  let fixture: ComponentFixture<OwntitlesComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [OwntitlesComponent]
    });
    fixture = TestBed.createComponent(OwntitlesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
