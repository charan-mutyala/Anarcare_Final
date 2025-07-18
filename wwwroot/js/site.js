// ✅ AOS Scroll Init
AOS.init({
  duration: 1000,
  once: false
});

// ✅ Toggle Accessibility View
function toggleAccessibilityView() {
  document.body.classList.toggle('accessibility-view');
}

document.addEventListener("DOMContentLoaded", function () {

  // ✅ Contact Form AJAX Submission
  const contactForm = document.getElementById("contactForm");
  const formMessage = document.getElementById("formMessage");

  if (contactForm && formMessage) {
    contactForm.addEventListener("submit", function (e) {
      e.preventDefault(); // prevent default reload

      const formData = new FormData(contactForm);

      fetch(contactForm.action, {
        method: "POST",
        body: formData
      })
      .then(res => res.text())
      .then(data => {
        if (data.trim() === "success") {
          formMessage.innerHTML = '<div class="alert alert-success">Thank you for contacting us!</div>';
          contactForm.reset();
        } else {
          formMessage.innerHTML = '<div class="alert alert-danger">Something went wrong.</div>';
        }
      })
      .catch(() => {
        formMessage.innerHTML = '<div class="alert alert-danger">Submission failed. Please try again.</div>';
      });
    });
  }

  
  // 🌐 Translate Button Popup Toggle
  const toggleBtn = document.getElementById("toggleTranslatePopup");
  const popup = document.getElementById("languagePopup");

  if (toggleBtn && popup) {
    toggleBtn.addEventListener("click", () => {
      popup.style.display = popup.style.display === "block" ? "none" : "block";
    });

    // ❗ Hide popup if clicked outside
    document.addEventListener("click", function (e) {
      if (!e.target.closest(".translate-container")) {
        popup.style.display = "none";
      }
    });
  }

  // 🌐 Simple Language Change Handler
  function changeLanguage(lang) {
    if (popup) popup.style.display = "none";
    
    // Wait for page to be fully loaded
    setTimeout(() => {
      // Find the Google Translate select element
      const selectField = document.querySelector('.goog-te-combo');
      
      if (selectField) {
        selectField.value = lang;
        selectField.dispatchEvent(new Event('change'));
        console.log("Language changed to:", lang);
      } else {
        console.log("Google Translate not ready, retrying...");
        setTimeout(() => changeLanguage(lang), 1000);
      }
    }, 500);
  }

  // 🌐 Language button event listeners
  document.querySelectorAll(".lang-btn").forEach((btn) => {
    btn.addEventListener("click", (e) => {
      e.preventDefault();
      const lang = btn.getAttribute("data-lang");
      changeLanguage(lang);
    });
  });

  // ✅ Mobile Nav Toggle
  const toggle = document.getElementById('mobileToggle');
  const nav = document.getElementById('mainNav');
  const phoneBanner = document.getElementById('phoneBanner');
  const toggleIcon = document.getElementById('toggleIcon');

  if (phoneBanner && window.innerWidth > 1339) {
    phoneBanner.classList.add('force-show');
  }

  if (toggle && nav && phoneBanner && toggleIcon) {
    toggle.addEventListener('click', () => {
      nav.classList.toggle('show');
      phoneBanner.classList.toggle('show');
      const isOpen = nav.classList.contains('show');
      toggleIcon.className = isOpen ? 'bi bi-x' : 'bi bi-list';
      document.body.classList.toggle('menu-open', isOpen);
    });
  }

  // ✅ Close nav on anchor click
  const navLinks = document.querySelectorAll('.main-nav a[href^="#"]');
  navLinks.forEach(link => {
    link.addEventListener('click', () => {
      if (nav.classList.contains('show')) {
        nav.classList.remove('show');
        phoneBanner?.classList.remove('show');
        toggleIcon.className = 'bi bi-list';
        document.body.classList.remove('menu-open');
      }
    });
  });

  // ✅ Dropdown toggle (mobile)

  const allDropdowns = document.querySelectorAll('.main-nav .dropdown');

  allDropdowns.forEach(dropdown => {
      const toggle = dropdown.querySelector('.dropdown-toggle');

      // --- Handle mouse entering the dropdown area (for DESKTOP HOVER) ---
      dropdown.addEventListener('mouseenter', () => {
          if (window.innerWidth >= 1400) {
              dropdown.classList.add('open');
          }
      });

      // --- Handle mouse leaving the dropdown area (for DESKTOP HOVER) ---
      dropdown.addEventListener('mouseleave', () => {
          if (window.innerWidth >= 1400) {
              // Only close if it's not "sticky" from a click
              if (dropdown.dataset.sticky !== 'true') {
                  dropdown.classList.remove('open');
              }
          }
      });

      // --- Handle CLICKS on the arrow button ---
      if (toggle) {
          toggle.addEventListener('click', e => {
              e.preventDefault();
              e.stopPropagation(); // Stop click from bubbling up to the document

              // Desktop "sticky" click behavior
              if (window.innerWidth >= 1400) {
                  if (dropdown.dataset.sticky === 'true') {
                      dropdown.dataset.sticky = 'false';
                      dropdown.classList.remove('open');
                  } else {
                      allDropdowns.forEach(d => { // First, unstick any others
                          d.classList.remove('open');
                          d.dataset.sticky = 'false';
                      });
                      dropdown.dataset.sticky = 'true'; // Make current one sticky
                      dropdown.classList.add('open');
                  }
              } 
              // Mobile click behavior
              else {
                  const isOpen = dropdown.classList.contains('open');
                  allDropdowns.forEach(d => d.classList.remove('open')); // Close all others
                  if (!isOpen) {
                      dropdown.classList.add('open'); // Re-open if it was closed
                  }
              }
          });
      }
  });

  // --- Handle clicks anywhere else on the page to close sticky menus ---
  document.addEventListener('click', () => {
      allDropdowns.forEach(d => {
          if (d.dataset.sticky === 'true') {
              d.dataset.sticky = 'false';
              d.classList.remove('open');
          }
      });
  });

  // ✅ OtherService input toggle
  const serviceDropdown = document.querySelector('select[name="ServicesNeeded"]');
  const otherServiceInput = document.querySelector('input[name="OtherService"]');

  if (serviceDropdown && otherServiceInput) {
    function toggleOtherServiceField() {
      const selected = serviceDropdown.value;
      if (selected === "Other") {
        otherServiceInput.style.display = "block";
      } else {
        otherServiceInput.style.display = "none";
        otherServiceInput.value = "";
      }
    }

    toggleOtherServiceField();
    serviceDropdown.addEventListener('change', toggleOtherServiceField);
  }
});

// ✅ Smooth Scroll Utility
function scrollToRole(id) {
  const element = document.getElementById(id);
  if (element) {
    element.scrollIntoView({ behavior: 'smooth', block: 'start' });
  }
}

// ✅ Reset layout on resize
window.addEventListener('resize', () => {
  const nav = document.getElementById('mainNav');
  const phoneBanner = document.getElementById('phoneBanner');
  const toggleIcon = document.getElementById('toggleIcon');

  if (window.innerWidth > 1339) {
    document.body.classList.remove('menu-open');
    nav?.classList.remove('show');
    phoneBanner?.classList.add('force-show');
    toggleIcon.className = 'bi bi-list';
  }
});

// 🌐 Global function to manually trigger language change (for debugging)
window.debugChangeLanguage = function(lang) {
  console.log("🐛 Debug: Manually changing language to:", lang);
  const selectField = document.querySelector(".goog-te-combo");
  if (selectField) {
    selectField.value = lang;
    selectField.dispatchEvent(new Event("change", { bubbles: true }));
    console.log("🐛 Debug: Language change triggered");
  } else {
    console.log("🐛 Debug: No translate dropdown found");
  }

};

  