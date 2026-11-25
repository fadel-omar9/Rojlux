/* ============================================
   ROJ LUX ASBL — Gallery JavaScript
   ============================================ */

document.addEventListener('DOMContentLoaded', function () {

  // ========== LIGHTBOX ==========
  const lightbox = document.getElementById('lightbox');
  const lightboxImg = document.getElementById('lightbox-img');
  const lightboxClose = document.querySelector('.lightbox-close');
  const lightboxPrev = document.querySelector('.lightbox-prev');
  const lightboxNext = document.querySelector('.lightbox-next');
  const galleryItems = document.querySelectorAll('.gallery-item');

  let currentIndex = 0;
  let filteredItems = Array.from(galleryItems);

  // Open lightbox
  galleryItems.forEach(function (item, index) {
    item.addEventListener('click', function () {
      currentIndex = filteredItems.indexOf(item);
      if (currentIndex === -1) currentIndex = 0;
      openLightbox(item);
    });
  });

  function openLightbox(item) {
    if (!lightbox || !lightboxImg) return;
    const img = item.querySelector('img');
    if (!img) return;
    lightboxImg.src = img.src;
    lightboxImg.alt = img.alt || '';
    lightbox.classList.add('active');
    document.body.style.overflow = 'hidden';
  }

  function closeLightbox() {
    if (!lightbox) return;
    lightbox.classList.remove('active');
    document.body.style.overflow = '';
  }

  function navigateLightbox(direction) {
    currentIndex = (currentIndex + direction + filteredItems.length) % filteredItems.length;
    const item = filteredItems[currentIndex];
    const img = item.querySelector('img');
    if (img && lightboxImg) {
      lightboxImg.style.opacity = '0';
      setTimeout(function () {
        lightboxImg.src = img.src;
        lightboxImg.alt = img.alt || '';
        lightboxImg.style.opacity = '1';
      }, 200);
    }
  }

  if (lightboxClose) {
    lightboxClose.addEventListener('click', closeLightbox);
  }

  if (lightboxPrev) {
    lightboxPrev.addEventListener('click', function () {
      navigateLightbox(-1);
    });
  }

  if (lightboxNext) {
    lightboxNext.addEventListener('click', function () {
      navigateLightbox(1);
    });
  }

  if (lightbox) {
    lightbox.addEventListener('click', function (e) {
      if (e.target === lightbox) {
        closeLightbox();
      }
    });
  }

  // Keyboard navigation
  document.addEventListener('keydown', function (e) {
    if (!lightbox || !lightbox.classList.contains('active')) return;
    if (e.key === 'Escape') closeLightbox();
    if (e.key === 'ArrowLeft') navigateLightbox(-1);
    if (e.key === 'ArrowRight') navigateLightbox(1);
  });

  // ========== GALLERY FILTER ==========
  const filterBtns = document.querySelectorAll('.gallery-filter-btn');

  filterBtns.forEach(function (btn) {
    btn.addEventListener('click', function () {
      // Active state
      filterBtns.forEach(function (b) { b.classList.remove('active'); });
      btn.classList.add('active');

      const filter = btn.getAttribute('data-filter');

      galleryItems.forEach(function (item) {
        const category = item.getAttribute('data-category');
        if (filter === 'all' || category === filter) {
          item.style.display = '';
          item.style.animation = 'scale-in 0.4s ease forwards';
        } else {
          item.style.display = 'none';
        }
      });

      // Update filtered items for lightbox navigation
      filteredItems = Array.from(galleryItems).filter(function (item) {
        return item.style.display !== 'none';
      });
    });
  });

  // ========== LAZY LOADING ==========
  const lazyImages = document.querySelectorAll('img[data-src]');
  if (lazyImages.length > 0) {
    const lazyObserver = new IntersectionObserver(function (entries) {
      entries.forEach(function (entry) {
        if (entry.isIntersecting) {
          const img = entry.target;
          img.src = img.getAttribute('data-src');
          img.removeAttribute('data-src');
          img.classList.add('loaded');
          lazyObserver.unobserve(img);
        }
      });
    }, { rootMargin: '200px' });

    lazyImages.forEach(function (img) {
      lazyObserver.observe(img);
    });
  }

});
