import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ArtistCreateEditComponent } from './artist-create-edit.component';

describe('ArtistCreateEditComponent', () => {
  let component: ArtistCreateEditComponent;
  let fixture: ComponentFixture<ArtistCreateEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ArtistCreateEditComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ArtistCreateEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
