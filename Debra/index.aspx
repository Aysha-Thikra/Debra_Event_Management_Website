<%@ Page Title="" Language="C#" MasterPageFile="~/index.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Debra.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="hero">
        <div class="hero-overlay"></div>
        <div class="hero-content">
            <h1>Welcome to Debra</h1>
            <p>
                Experience world-class musical events featuring top musicians and bands from around the globe. Let us
                turn your events into unforgettable experiences!
            </p>
            <div class="cta-buttons">
                <a href="GuestPage/event.aspx" class="cta-btn" style="text-decoration:none;">Explore Events</a>
            </div>
        </div>
    </section>

    <!-- Slider section -->
    <section class="event-slider" id="events">
        <h2>Upcoming Events</h2>
        <div class="slider-container">
            <div class="slider">
                <% foreach (var ev in events)
                    { %>
                <div class="slide">
                    <div class="event-card">
                        <img src="<%= ResolveUrl("~/images/events/" + ev.EventPoster) %>" alt="<%= ev.EventName %>">
                        <div class="event-details">
                            <h3><i class="fas fa-music"></i>&nbsp;<%= ev.EventName %></h3>
                            <p><i class="fas fa-calendar-day"></i>&nbsp;Date: &nbsp;<%= ev.EventDate.ToString("MMMM dd, yyyy") %></p>
                            <p><i class="fas fa-map-marker-alt"></i>&nbsp;Location:&nbsp; <%= ev.Location %></p>
                            <h4><i class="fas fa-tag"></i>&nbsp;Price: RS. <%= ev.Price.ToString("F2") %></h4>
                            <a href="buy-ticket.aspx?eventId=<%= ev.EventID %>" class="cta-btn" style="text-decoration: none"><i class="fas fa-ticket-alt"></i> &nbsp;Buy Ticket</a>
                        </div>
                    </div>

                </div>
                <% } %>
            </div>
            <button class="prev" onclick="moveSlide(-1)">&#10094;</button>
            <button class="next" onclick="moveSlide(1)">&#10095;</button>
        </div>
    </section>



    <section class="event-categories">
        <h2>Music Event Categories</h2>
        <div class="categories-container">
            <div class="category-card">
                <img src="images/categories/rock.jpg" alt="Rock Music Events">
                <div class="category-info">
                    <h3>Rock</h3>
                    <p>
                        Experience electrifying rock concerts and live performances from top rock bands around the world.
                        Whether you're a fan of classic rock or modern hits, there's something for everyone.
                    </p>
                </div>
            </div>
            <div class="category-card">
                <img src="images/categories/jazz.jpg" alt="Jazz Music Events">
                <div class="category-info">
                    <h3>Jazz</h3>
                    <p>
                        Immerse yourself in the soulful rhythms of jazz. From smooth, relaxing sessions to lively and
                        upbeat performances, our jazz events offer a variety of styles and moods.
                    </p>
                </div>
            </div>
            <div class="category-card">
                <img src="images/categories/classical.jpg" alt="Classical Music Events">
                <div class="category-info">
                    <h3>Classical</h3>
                    <p>
                        Delight in the beauty of classical music with performances by world-class orchestras and
                        soloists. Enjoy timeless compositions and experience the grandeur of classical concerts.
                    </p>
                </div>
            </div>
            <div class="category-card">
                <img src="images/categories/hip-hop.jpg" alt="Hip-Hop Music Events">
                <div class="category-info">
                    <h3>Hip-Hop</h3>
                    <p>
                        Get ready for high-energy hip-hop events featuring the latest rap battles and performances from
                        top artists in the genre. Feel the beat and experience the excitement of live hip-hop shows.
                    </p>
                </div>
            </div>
            <div class="category-card">
                <img src="images/categories/electronic.jpg" alt="Electronic Music Events">
                <div class="category-info">
                    <h3>Electronic</h3>
                    <p>
                        Discover the pulsating rhythms of electronic music. Our electronic events feature DJs and live
                        electronic acts that bring vibrant beats and high-energy performances.
                    </p>
                </div>
            </div>
            <div class="category-card">
                <img src="images/categories/blues.jpeg" alt="Blues Music Events">
                <div class="category-info">
                    <h3>Blues</h3>
                    <p>
                        Enjoy soulful blues performances that capture the essence of this genre. From acoustic sessions
                        to full band performances, experience the depth and emotion of blues music.
                    </p>
                </div>
            </div>
        </div>
    </section>

    <section class="featured-venues">
        <h2>Featured Venues in Sri Lanka</h2>
        <p>
            Explore our selection of top venues across Sri Lanka that are perfect for hosting unforgettable concerts and
            events. Each venue offers a unique atmosphere and excellent facilities to ensure a memorable experience for
            both artists and audiences.
        </p>
        <div class="venues-container">
            <div class="venue-card">
                <img src="images/venues/The Colombo Music Hall.jpeg" alt="Venue 1">
                <div class="venue-info">
                    <h3>The Colombo Music Hall</h3>
                    <p>
                        Located in the heart of Colombo, this modern venue features state-of-the-art sound systems and
                        comfortable seating for large audiences. Perfect for both international and local concerts.
                    </p>
                </div>
            </div>
            <div class="venue-card">
                <img src="images/venues/Kandy Royal Theatre.jpg" alt="Venue 2">
                <div class="venue-info">
                    <h3>Kandy Royal Theatre</h3>
                    <p>
                        With its rich history and charming architecture, the Kandy Royal Theatre offers an elegant
                        setting for classical music and cultural performances. It combines historic charm with modern
                        amenities.
                    </p>
                </div>
            </div>
            <div class="venue-card">
                <img src="images/venues/Galle Fort Arena.jpg" alt="Venue 3">
                <div class="venue-info">
                    <h3>Galle Fort Arena</h3>
                    <p>
                        This open-air venue, located within the historic Galle Fort, is ideal for large outdoor concerts
                        and festivals. The venue offers a unique backdrop and a spacious area for a vibrant atmosphere.
                    </p>
                </div>
            </div>
            <div class="venue-card">
                <img src="images/venues/Negombo Beach Pavilion.jpg" alt="Venue 4">
                <div class="venue-info">
                    <h3>Negombo Beach Pavilion</h3>
                    <p>
                        Situated by the picturesque Negombo Beach, this pavilion is perfect for sunset concerts and
                        beachside events. It offers a relaxed atmosphere with stunning views of the ocean.
                    </p>
                </div>
            </div>
            <div class="venue-card">
                <img src="images/venues/Jaffna Cultural Centre.jpg" alt="Venue 5">
                <div class="venue-info">
                    <h3>Jaffna Cultural Centre</h3>
                    <p>
                        Located in Jaffna, this cultural centre combines traditional architecture with modern facilities,
                        making it an excellent venue for classical and cultural performances.
                    </p>
                </div>
            </div>
            <div class="venue-card">
                <img src="images/venues/Viharamahadevi Open Air Theatre.jpg" alt="Venue 6">
                <div class="venue-info">
                    <h3>Viharamahadevi Open Air Theatre</h3>
                    <p>
                        Situated in Colombo, this open-air theatre is known for its spacious seating and fantastic city
                        views. It is ideal for large-scale events and concerts under the stars.
                    </p>
                </div>
            </div>
        </div>
    </section>

    <script>
        let currentSlide = 0;
        const slides = document.querySelectorAll('.slide');
        const totalSlides = slides.length;
        const slideWidth = slides[0].offsetWidth; 

        function showSlide(index) {
            if (index >= totalSlides) {
                currentSlide = 0;
            } else if (index < 0) {
                currentSlide = totalSlides - 1;
            } else {
                currentSlide = index;
            }

            const newTransformValue = -currentSlide * slideWidth;
            document.querySelector('.slider').style.transform = `translateX(${newTransformValue}px)`;
        }

        function moveSlide(step) {
            showSlide(currentSlide + step);
        }

        showSlide(currentSlide);

        let isDragging = false;
        let startX = 0;

        const sliderContainer = document.querySelector('.slider-container');

        sliderContainer.addEventListener('mousedown', (event) => {
            isDragging = true;
            startX = event.pageX;
        });

        sliderContainer.addEventListener('mouseup', (event) => {
            if (!isDragging) return;
            const endX = event.pageX;
            const distance = endX - startX;

            if (distance > slideWidth / 2) {
                moveSlide(-1); 
            } else if (distance < -slideWidth / 2) {
                moveSlide(1); 
            }
            isDragging = false;
        });

        sliderContainer.addEventListener('mouseleave', () => {
            isDragging = false;
        });

        sliderContainer.addEventListener('touchstart', (event) => {
            isDragging = true;
            startX = event.touches[0].clientX;
        });

        sliderContainer.addEventListener('touchend', (event) => {
            if (!isDragging) return;
            const endX = event.changedTouches[0].clientX;
            const distance = endX - startX;

            if (distance > slideWidth / 2) {
                moveSlide(-1); 
            } else if (distance < -slideWidth / 2) {
                moveSlide(1); 
            }
            isDragging = false;
        });

        window.addEventListener('resize', () => {
            slideWidth = slides[0].offsetWidth;
        });

    </script>

</asp:Content>
