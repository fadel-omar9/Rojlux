/* ============================================
   ROJ LUX ASBL — Form Handling
   ============================================ */

document.addEventListener('DOMContentLoaded', function () {

  // ========== MEMBERSHIP FORM ==========
  const memberForm = document.getElementById('membership-form');
  const formSuccess = document.getElementById('form-success');

  if (memberForm) {
    memberForm.addEventListener('submit', function (e) {
      e.preventDefault();

      // Clear errors
      document.querySelectorAll('.form-group.error').forEach(function (g) {
        g.classList.remove('error');
      });

      // Validate
      let isValid = true;

      const requiredFields = memberForm.querySelectorAll('[required]');
      requiredFields.forEach(function (field) {
        if (!field.value.trim()) {
          const group = field.closest('.form-group');
          if (group) group.classList.add('error');
          isValid = false;
        }
      });

      // Email validation
      const email = memberForm.querySelector('#member-email');
      if (email && email.value && !isValidEmail(email.value)) {
        const group = email.closest('.form-group');
        if (group) {
          group.classList.add('error');
          const errorEl = group.querySelector('.form-error');
          if (errorEl) errorEl.textContent = 'Bitte geben Sie eine gültige E-Mail-Adresse ein.';
        }
        isValid = false;
      }

      // Checkbox
      const consent = memberForm.querySelector('#consent-checkbox');
      if (consent && !consent.checked) {
        const group = consent.closest('.form-group');
        if (group) group.classList.add('error');
        isValid = false;
      }

      if (!isValid) {
        const firstError = memberForm.querySelector('.form-group.error');
        if (firstError) {
          firstError.scrollIntoView({ behavior: 'smooth', block: 'center' });
        }
        return;
      }

      // Collect form data
      const data = new FormData(memberForm);
      const formData = {};
      data.forEach(function (value, key) {
        formData[key] = value;
      });

      // Show loading state
      const submitBtn = memberForm.querySelector('button[type="submit"]');
      const originalText = submitBtn.innerHTML;
      submitBtn.innerHTML = '<span class="spinner"></span> Wird gesendet...';
      submitBtn.disabled = true;

      // Simulate sending (replace with EmailJS or backend)
      setTimeout(function () {
        // In production, replace with:
        // emailjs.send('service_id', 'template_id', formData)
        console.log('Membership form data:', formData);

        memberForm.style.display = 'none';
        if (formSuccess) formSuccess.classList.add('show');

        submitBtn.innerHTML = originalText;
        submitBtn.disabled = false;
      }, 1500);
    });
  }

  // ========== CONTACT FORM ==========
  const contactForm = document.getElementById('contact-form');
  const contactSuccess = document.getElementById('contact-success');

  if (contactForm) {
    contactForm.addEventListener('submit', function (e) {
      e.preventDefault();

      // Clear errors
      document.querySelectorAll('.form-group.error').forEach(function (g) {
        g.classList.remove('error');
      });

      let isValid = true;

      const requiredFields = contactForm.querySelectorAll('[required]');
      requiredFields.forEach(function (field) {
        if (!field.value.trim()) {
          const group = field.closest('.form-group');
          if (group) group.classList.add('error');
          isValid = false;
        }
      });

      const email = contactForm.querySelector('#contact-email');
      if (email && email.value && !isValidEmail(email.value)) {
        const group = email.closest('.form-group');
        if (group) group.classList.add('error');
        isValid = false;
      }

      if (!isValid) {
        const firstError = contactForm.querySelector('.form-group.error');
        if (firstError) {
          firstError.scrollIntoView({ behavior: 'smooth', block: 'center' });
        }
        return;
      }

      const data = new FormData(contactForm);
      const formData = {};
      data.forEach(function (value, key) {
        formData[key] = value;
      });

      const submitBtn = contactForm.querySelector('button[type="submit"]');
      const originalText = submitBtn.innerHTML;
      submitBtn.innerHTML = '<span class="spinner"></span> Wird gesendet...';
      submitBtn.disabled = true;

      setTimeout(function () {
        console.log('Contact form data:', formData);

        contactForm.style.display = 'none';
        if (contactSuccess) contactSuccess.classList.add('show');

        submitBtn.innerHTML = originalText;
        submitBtn.disabled = false;
      }, 1500);
    });
  }

  // ========== HELPER ==========
  function isValidEmail(email) {
    return /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email);
  }

});
