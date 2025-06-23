// Initialize AOS (Animate On Scroll)
AOS.init({
  duration: 1000,
  once: false
});

document.addEventListener('DOMContentLoaded', function () {
  const toggle = document.getElementById('mobileToggle');
  const nav = document.getElementById('mainNav');
  const phoneBanner = document.getElementById('phoneBanner');
  const toggleIcon = document.getElementById('toggleIcon');

  // Ensure phone banner is always visible on desktop
  if (phoneBanner && window.innerWidth > 1005) {
    phoneBanner.classList.add('force-show');
  }

  // Toggle mobile nav menu
  if (toggle && nav && phoneBanner && toggleIcon) {
    toggle.addEventListener('click', () => {
      nav.classList.toggle('show');
      phoneBanner.classList.toggle('show');
      const isOpen = nav.classList.contains('show');
      toggleIcon.className = isOpen ? 'bi bi-x' : 'bi bi-list';
      document.body.classList.toggle('menu-open', isOpen);
    });
  }

  // Close nav when clicking a link
  const navLinks = document.querySelectorAll('.main-nav a[href^="#"]');
  navLinks.forEach(link => {
    link.addEventListener('click', () => {
      if (nav && nav.classList.contains('show')) {
        nav.classList.remove('show');
        phoneBanner?.classList.remove('show');
        if (toggleIcon) toggleIcon.className = 'bi bi-list';
        document.body.classList.remove('menu-open');
      }
    });
  });

  // Toggle dropdown only via arrow icon on mobile
  const dropdownToggles = document.querySelectorAll('.dropdown-toggle');
  dropdownToggles.forEach(button => {
    button.addEventListener('click', function (e) {
      if (window.innerWidth <= 1005) {
        e.preventDefault();
        const parentLi = this.closest('.dropdown');
        parentLi?.classList.toggle('open');
      }
    });
  });
});

// Smooth scroll to a role section (used in About/Career pages)
function scrollToRole(id) {
  const element = document.getElementById(id);
  if (element) {
    element.scrollIntoView({ behavior: 'smooth', block: 'start' });
  }
}

// Clean up menu state on window resize
window.addEventListener('resize', () => {
  const nav = document.getElementById('mainNav');
  const phoneBanner = document.getElementById('phoneBanner');
  const toggleIcon = document.getElementById('toggleIcon');

  if (window.innerWidth > 1005) {
    document.body.classList.remove('menu-open');
    nav?.classList.remove('show');
    phoneBanner?.classList.add('force-show');
    if (toggleIcon) toggleIcon.className = 'bi bi-list';
  }
});
